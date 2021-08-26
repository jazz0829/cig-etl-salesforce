using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Configuration.Upload;
using Eol.Cig.Etl.Salesforce.Upload;
using Eol.Cig.Etl.Shared.Utils;
using log4net;
using Task = System.Threading.Tasks.Task;

namespace Eol.Cig.Etl.Salesforce.Jobs.Upload
{
    public class KeyEventsUpload : IEtlJob
    {
        public static readonly string Name = nameof(KeyEventsUpload);

        protected readonly ISalesforceUploaderFactory _salesforceUploaderFactory;
        protected readonly ILog _logger;
        protected readonly ISalesforceUploadJobConfiguration _jobConfiguration;
        protected readonly int _batchSize;
        protected readonly string _rowNumberColumnName;
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";


        public KeyEventsUpload(ILog logger,
            ISalesforceUploadConfiguration jobConfiguration,
            ISalesforceUploaderFactory salesforceUploaderFactory)
        {
            _logger = logger;
            _salesforceUploaderFactory = salesforceUploaderFactory;
            _jobConfiguration = (jobConfiguration ?? throw new ArgumentNullException(nameof(jobConfiguration))).GetJobConfig(GetName());
            _batchSize = jobConfiguration.BatchSize;
            _rowNumberColumnName = jobConfiguration.RowNumberColumnName;
        }

        public string GetName()
        {
            return Name;
        }

        public async Task Execute()
        {
            var connectionStr = _jobConfiguration.SourceConnectionString;
            var tableName = _jobConfiguration.SourceTable;

            var synctime = DateTime.UtcNow.ToString(DateTimeFormat);
            var rowCount = GetTableRowCount();

            long bookmark = GetJobBookmark();
            long nonSyncCount = rowCount - bookmark;

            if (nonSyncCount > 0)
            {
                for (var i = bookmark; i < rowCount; i += _batchSize)
                {
                    var table = new DataTable();
                    using (var sqlConn = new SqlConnection(connectionStr))
                    {
                        var sqlQuery = $"SELECT {_rowNumberColumnName},Id,ContactID__c,AccountantLinked__c,LegislationTemplateCreated__c,FirstMoblieAppUsage__c,FirstPurchaseEntry__c,FirstBankStatementImport__c,FirstProjectSalesInvoice__c," +
                                      $"FirstOpportunity__c,FirstTimeEntry__c,FirstProject__c,FirstSalesInvoice__c,FirstConsult__c,FirstConsultRequested__c,FirstConsultPlanned__c FROM {tableName} (nolock) " +
                                      $"WHERE {_rowNumberColumnName} BETWEEN {i + 1} and {i + _batchSize}";

                        using (var cmd = new SqlCommand(sqlQuery, sqlConn))
                        {
                            var ds = new SqlDataAdapter(cmd);

                            ds.Fill(table);

                            var toInsertRows = table.Select("Id is null");
                            var toUpdateRows = table.Select("Id is not null");

                            var toInsertTable = table.Clone();
                            var toUpdateTable = table.Clone();

                            toInsertRows.ToList().ForEach(item => toInsertTable.ImportRow(item));
                            toUpdateRows.ToList().ForEach(item => toUpdateTable.ImportRow(item));

                            string updateQuery;
                            if (toInsertTable.Rows.Count > 0)
                            {
                                var salesforceInsertUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject, JobOperation.Insert);
                                var refereceInsertTable = toInsertTable.Copy();
                                toInsertTable.Columns.Remove("Id");
                                toInsertTable.Columns.Remove(_rowNumberColumnName);

                                var insertResults = salesforceInsertUploader.GetResults(toInsertTable).ToList();
                                var insertedRecords = new List<string>();
                                for (var index = 0; index < refereceInsertTable.Rows.Count; index++)
                                {
                                    DataRow row = refereceInsertTable.Rows[index];
                                    if (insertResults[index].Success)
                                    {
                                        insertedRecords.Add(row.Field<long>(_rowNumberColumnName).ToString());

                                    }
                                }

                                var bulkUpdate = new StringBuilder();
                                if (insertedRecords.Count > 0)
                                {
                                    var insertedRowNumbers = insertedRecords.Aggregate((x, y) => x + "," + y);
                                    bulkUpdate.AppendLine($"UPDATE {tableName} SET ETLSyncTime = '{synctime}' WHERE {_rowNumberColumnName} IN ({insertedRowNumbers});");
                                }

                                foreach (var accountUploadResult in insertResults.Where(r => !r.Success))
                                {
                                    Console.WriteLine(accountUploadResult.Error);
                                }

                                var failedRecords = insertResults.Where(r => !r.Success).ToList();

                                var errorList = new StringBuilder();

                                if (failedRecords.Count > 0)
                                {
                                    failedRecords.ForEach(e => errorList.AppendLine(e.ToString()));

                                    _logger.Error($"Errors occured for some accounts while pushing the Next Best Actions \n: {errorList}");
                                }

                                updateQuery = bulkUpdate.ToString();

                                if (!string.IsNullOrEmpty(updateQuery))
                                {
                                    _logger.Info("Running query to update ETLSyncTime for successfully inserted records.");
                                    SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionStr, true);
                                }
                            }

                            if (toUpdateTable.Rows.Count > 0)
                            {
                                var salesforceUpdateUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject);
                                toUpdateTable.Columns.Remove(_rowNumberColumnName);

                                var updateResults = salesforceUpdateUploader.GetResults(toUpdateTable).ToList();
                                var updatedRecords = updateResults.Where(r => r.Success).ToList();
                                var failedRecords = updateResults.Where(r => !r.Success).ToList();

                                var errorList = new StringBuilder();
                                var bulkUpdate = new StringBuilder();

                                if (failedRecords.Count > 0)
                                {
                                    failedRecords.ForEach(e => errorList.AppendLine(e.ToString()));

                                    _logger.Error($"Errors occured while uploading Key Events to Salesforce\n: {errorList}");
                                }

                                if (updatedRecords.Count > 0)
                                {
                                    _logger.Info($"{updatedRecords.Count} Key Events were pushed successfully to Salesforce.");
                                    var updatedIdentifiers =$"'{string.Join("','", updatedRecords.Select(f => f.Id))}'";
                                    bulkUpdate.AppendLine($"UPDATE {tableName} SET ETLSyncTime = '{synctime}' WHERE Id in ({updatedIdentifiers});");
                                    updateQuery = bulkUpdate.ToString();
                                }
                                else
                                {
                                    updateQuery = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(updateQuery))
                                {
                                    _logger.Info("Running query to update ETLSyncTime for successfully updated records.");
                                    SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionStr, true);
                                }
                            }
                        }
                    }
                    var maxbookmark = GetLatestSyncRecord(synctime);
                    SaveJobBookmark(maxbookmark, synctime);
                }
            }

            await Task.CompletedTask;
        }

        private long GetJobBookmark()
        {
            long bookmark = 0;
            var query = $"SELECT TOP 1 RowNumberBookmark FROM config.Salesforce_UploadJobLog (nolock) WHERE JobName = '{GetName()}' and date ='{DateTime.Now.Date}' order by RowNumberBookmark desc";
            try
            {
                using (var reader = SqlServerUtils.ExecuteCommandReturnReader(query, _jobConfiguration.SourceConnectionString))
                {
                    while (reader.Read())
                    {
                        bookmark = reader.GetInt64(0);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured while trying to read the latest bookmark from config.Salesforce_UploadJobLog. See Stack: {ex}");
                throw;
            }

            return bookmark;
        }

        private long GetLatestSyncRecord(string synctime)
        {
            long bookmark = 0;
            var query = $"SELECT ISNULL(Max({_rowNumberColumnName}), 0) FROM publish.Salesforce_KeyEvents (nolock)  where ETLSyncTime='{synctime}'";
            try
            {
                using (var reader = SqlServerUtils.ExecuteCommandReturnReader(query, _jobConfiguration.SourceConnectionString))
                {
                    while (reader.Read())
                    {
                        bookmark = reader.GetInt64(0);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured while trying to read the latest bookmark from config.Salesforce_UploadJobLog. See Stack: {ex}");
                throw;
            }

            return bookmark;
        }

        private void SaveJobBookmark(long bookmark, string synctime)
        {
            var query = $"INSERT INTO config.Salesforce_UploadJobLog (JobName, RowNumberBookmark,Date,SyncTime) VALUES ('{GetName()}',{bookmark},'{DateTime.Now.Date}','{synctime}')";
            try
            {
                SqlServerUtils.ExecuteCommandReturnNone(query, _jobConfiguration.SourceConnectionString);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured while trying to read the latest bookmark from config.Salesforce_UploadJobLog. See Stack: {ex}");
                throw;
            }
        }

        private long GetTableRowCount()
        {
            long count;
            var sqlTable = _jobConfiguration.SourceTable;
            var rowCount = $"SELECT ISNULL(MAX({_rowNumberColumnName}), 0) FROM {sqlTable} (nolock)";

            try
            {
                count = SqlServerUtils.ExecuteCommandReturnSingle<long>(rowCount, _jobConfiguration.SourceConnectionString);
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured during counting row number of table" + sqlTable, ex);
                throw;
            }
            return count;
        }


    }
}
