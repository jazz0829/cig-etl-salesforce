using Eol.Cig.Etl.Salesforce.Configuration.Upload;
using Eol.Cig.Etl.Salesforce.Upload;
using Eol.Cig.Etl.Shared.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Eol.Cig.Etl.Salesforce.Jobs.Upload
{
    public class CustomerNextBestActionsUpload : IEtlJob
    {
        public static readonly string Name = nameof(CustomerNextBestActionsUpload);

        protected readonly ISalesforceUploaderFactory _salesforceUploaderFactory;
        protected readonly ILog _logger;
        protected readonly ISalesforceUploadJobConfiguration _jobConfiguration;
        protected readonly int _batchSize;
        protected readonly string _rowNumberColumnName;
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public CustomerNextBestActionsUpload(ILog logger, ISalesforceUploadConfiguration jobConfiguration, ISalesforceUploaderFactory salesforceUploaderFactory)
        {
            _logger = logger;
            _salesforceUploaderFactory = salesforceUploaderFactory;
            _jobConfiguration = (jobConfiguration ?? throw new ArgumentNullException(nameof(jobConfiguration))).GetJobConfig(GetName());
            _batchSize = jobConfiguration.BatchSize;
            _rowNumberColumnName = jobConfiguration.RowNumberColumnName;
        }

        public async Task Execute()
        {
            var connectionStr = _jobConfiguration.SourceConnectionString;
            var tableName = _jobConfiguration.SourceTable;

            var synctime = DateTime.UtcNow.ToString(DateTimeFormat);

            int rowCount = GetTableRowCount();

            long bookmark = GetJobBookmark();

            long nonSyncCount = rowCount - bookmark;

            if (nonSyncCount > 0)
            {
                for (var i = bookmark; i < rowCount; i += _batchSize)
                {
                    var table = new DataTable();
                    using (var sqlConn = new SqlConnection(connectionStr))
                    {
                        var sqlQuery = $"SELECT {_rowNumberColumnName}, Salesforce_ID as Id, AccountID as 'Account__r.Exact_ID__c', CI_EventID as CIG_ID__c, Suggest as Is_Suggested__c " +
                            $",NextBestActionID as 'Action__r.CIG_ID__c', DateTimeSentToSalesforce, DateTimeLastModified FROM {tableName} (nolock)" +
                            $" WHERE {_rowNumberColumnName} BETWEEN {i + 1} and {i + _batchSize}";

                        var updateQuery = string.Empty;

                        using (var cmd = new SqlCommand(sqlQuery, sqlConn))
                        {
                            var ds = new SqlDataAdapter(cmd);

                            ds.Fill(table);

                            var toInsertRows = table.Select("DateTimeSentToSalesforce is null");
                            var toUpdateRows = table.Select("DateTimeSentToSalesforce is not null and DateTimeSentToSalesforce < DateTimeLastModified");

                            var toInsertTable = table.Clone();
                            var toUpdateTable = table.Clone();

                            toInsertRows.ToList().ForEach(item => toInsertTable.ImportRow(item));
                            toUpdateRows.ToList().ForEach(item => toUpdateTable.ImportRow(item));

                            if (toInsertTable.Rows.Count > 0)
                            {
                                var salesforceInsertUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject, Client.JobOperation.Insert);
                                var toDeleteColumns = new List<string>
                                {
                                    "DateTimeSentToSalesforce",
                                    "DateTimeLastModified",
                                    "Id",
                                    _rowNumberColumnName
                                };

                                toDeleteColumns.ForEach(c => toInsertTable.Columns.Remove(c));

                                var insertResults = salesforceInsertUploader.GetResults(toInsertTable).ToList();

                                var bulkUpdate = new StringBuilder();

                                for (int k = 0; k < insertResults.Count; k++)
                                {
                                    var item = insertResults[k];
                                    if (item.Success)
                                    {
                                        bulkUpdate.AppendLine($"UPDATE {tableName} SET DateTimeSentToSalesforce = '{synctime}', Salesforce_ID = '{insertResults[k].Id}' WHERE {_rowNumberColumnName} = {toInsertRows[k][_rowNumberColumnName]};");
                                    }

                                    if (!item.Success && insertResults[k].Error.StartsWith("DUPLICATE_VALUE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        var id = item.Error.Replace("DUPLICATE_VALUE:duplicate value found: CIG_ID__c duplicates value on record with id: ", "").Replace(":--", "");
                                        if (id.Length <= 20)
                                        {
                                            bulkUpdate.AppendLine($"UPDATE {tableName} SET DateTimeSentToSalesforce = '{synctime}', Salesforce_ID = '{id}' WHERE {_rowNumberColumnName} = {toInsertRows[k][_rowNumberColumnName]};");
                                        }
                                    }
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
                                    _logger.Info("Running query to update DateTimeSentToSalesforce for successfully inserted records.");
                                    SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionStr, true);
                                }
                            }

                            if (toUpdateTable.Rows.Count > 0)
                            {
                                var salesforceUpdateUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject);

                                var toDeleteColumns = new List<string>
                                {
                                    _rowNumberColumnName,
                                    "DateTimeSentToSalesforce",
                                    "DateTimeLastModified",
                                    "Account__r.Exact_ID__c",
                                    "CIG_ID__c",
                                    "Action__r.CIG_ID__c"
                                };

                                toDeleteColumns.ForEach(c => toUpdateTable.Columns.Remove(c));

                                var updateResults = salesforceUpdateUploader.GetResults(toUpdateTable).ToList();

                                var updatedRecords = updateResults.Where(r => r.Success).ToList();

                                var failedRecords = updateResults.Where(r => !r.Success).ToList();

                                var errorList = new StringBuilder();
                                var bulkUpdate = new StringBuilder();

                                if (failedRecords.Count > 0)
                                {
                                    failedRecords.ForEach(e => errorList.AppendLine(e.ToString()));

                                    _logger.Error($"Errors occured for some accounts while pushing the Next Best Actions \n: {errorList}");
                                }

                                if (updatedRecords.Count > 0)
                                {
                                    _logger.Info($"The Next Best Actions were pushed successfully for {updatedRecords.Count} record.");

                                    foreach (var item in updatedRecords)
                                    {
                                        bulkUpdate.AppendLine($"UPDATE {tableName} SET DateTimeSentToSalesforce = '{synctime}' WHERE Salesforce_ID = '{item.Id}';");
                                    }

                                    updateQuery = bulkUpdate.ToString();
                                }
                                else
                                {
                                    updateQuery = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(updateQuery))
                                {
                                    _logger.Info("Running query to update DateTimeSentToSalesforce for successfully updated records.");
                                    SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionStr, true);
                                }
                            }
                        }

                        var updateEtlSyncTimeQuery = $"UPDATE {tableName} set EtlSyncTime='{synctime}' where RowNumber BETWEEN {i + 1} and {i + _batchSize}";

                        SqlServerUtils.ExecuteCommandReturnNone(updateEtlSyncTimeQuery, connectionStr, true);

                        var maxbookmark = GetLatestSyncRecord(synctime);
                        SaveJobBookmark(maxbookmark, synctime);
                    }
                }
            }

            await Task.CompletedTask;
        }

        private int GetTableRowCount()
        {
            int count;
            var query = $"SELECT COUNT (*) FROM {_jobConfiguration.SourceTable} (nolock)";

            try
            {
                count = SqlServerUtils.ExecuteCommandReturnInt(query, _jobConfiguration.SourceConnectionString);
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured during counting row number of table" + _jobConfiguration.SourceTable, ex);
                throw;
            }
            return count;
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
            var query = $"SELECT ISNULL(Max({_rowNumberColumnName}), 0) FROM {_jobConfiguration.SourceTable} (nolock)  where EtlSyncTime='{synctime}'";
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

        public string GetName()
        {
            return Name;
        }
    }
}
