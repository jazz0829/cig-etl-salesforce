using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Eol.Cig.Etl.Salesforce.Upload;
using Eol.Cig.Etl.Shared.Utils;
using Task = System.Threading.Tasks.Task;
using log4net;
using System.Text;
using Eol.Cig.Etl.Salesforce.Configuration.Upload;

namespace Eol.Cig.Etl.Salesforce.Jobs.Upload
{
    public class HealthScoreUpload : IEtlJob
    {
        public static readonly string Name = nameof(HealthScoreUpload);

        protected readonly ISalesforceUploaderFactory _salesforceUploaderFactory;
        protected readonly ILog _logger;
        protected readonly ISalesforceUploadJobConfiguration _jobConfiguration;
        protected readonly int _batchSize;
        protected readonly string _rowNumberColumnName;
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public HealthScoreUpload(ILog logger,
           ISalesforceUploadConfiguration jobConfiguration,
           ISalesforceUploaderFactory salesforceUploaderFactory)
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

            int rowCount = GetTableRowCount(tableName, connectionStr);
            long bookmark = GetJobBookmark();
            long nonSyncCount = rowCount - bookmark;

            if (nonSyncCount > 0)
            {
                for (var i = bookmark; i < rowCount; i += _batchSize)
                {
                    var table = new DataTable();
                    using (var sqlConn = new SqlConnection(connectionStr))
                    {
                        var sqlQuery = $"SELECT Id, Customer_Score__c FROM {tableName} (nolock) WHERE {_rowNumberColumnName} BETWEEN {i + 1} and {i + _batchSize}";

                        using (var cmd = new SqlCommand(sqlQuery, sqlConn))
                        {
                            var ds = new SqlDataAdapter(cmd);

                            ds.Fill(table);

                            var salesforceUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject);
                            var results = salesforceUploader.GetResults(table).ToList();

                            string updateQuery;
                            if (results.All(c => c.Success))
                            {
                                _logger.Info($"Engagment score was pushed successfully for {table.Rows.Count}");
                                updateQuery = $"UPDATE {tableName} SET ETLSyncTime = '{synctime}' WHERE {_rowNumberColumnName} BETWEEN {i + 1} and {i + _batchSize}";
                            }
                            else
                            {
                                var updatedIds = results.Where(r => r.Success).Select(c => c.Id).ToList();
                                var errors = results.Where(r => !r.Success).ToList();
                                var sb = new StringBuilder();
                                errors.ForEach(e => sb.AppendLine(e.ToString()));
                                _logger.Error($"Errors occured for some accounts while pushing the engagment score \n: {sb.ToString()}");
                                if (updatedIds.Count > 0)
                                {
                                    _logger.Info($"Engagment score was pushed successfully for {updatedIds.Count}");
                                    updateQuery = $"UPDATE {tableName} SET ETLSyncTime = '{synctime}' WHERE Id in ({string.Join(",", updatedIds.Select(c => $"'{c}'").ToList())})";
                                }
                                else
                                {
                                    updateQuery = string.Empty;
                                }
                            }

                            if (!string.IsNullOrEmpty(updateQuery))
                            {
                                _logger.Info($"Running this query to update ETLSyncTime: {updateQuery}");
                                SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionStr, true);
                            }
                        }
                    }

                    var maxbookmark = GetLatestSyncRecord(synctime);
                    SaveJobBookmark(maxbookmark, synctime);
                }
            }

            await Task.CompletedTask;
        }
        private long GetLatestSyncRecord(string synctime)
        {
            long bookmark = 0;
            var query = $"SELECT ISNULL(Max({_rowNumberColumnName}), 0) FROM {_jobConfiguration.SourceTable} (nolock)  where ETLSyncTime='{synctime}'";
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

        private int GetTableRowCount(string sqlTable, string sqlConnectionName)
        {
            int count;
            var rowCount = $"SELECT COUNT (*) FROM {sqlTable} (nolock)";

            try
            {
                count = SqlServerUtils.ExecuteCommandReturnInt(rowCount, sqlConnectionName);
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured during counting row number of table" + sqlTable, ex);
                throw;
            }
            return count;
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

        public string GetName()
        {
            return Name;
        }
    }
}
