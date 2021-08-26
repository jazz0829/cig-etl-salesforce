using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eol.Cig.Etl.Salesforce.Configuration.Upload;
using Eol.Cig.Etl.Salesforce.Upload;
using Eol.Cig.Etl.Shared.Utils;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Jobs.Upload
{
    public class CaseReasonsUpload : IEtlJob
    {
        public static readonly string Name = nameof(CaseReasonsUpload);

        protected readonly ISalesforceUploaderFactory _salesforceUploaderFactory;
        protected readonly ILog _logger;
        protected readonly ISalesforceUploadJobConfiguration _jobConfiguration;
        protected readonly int _batchSize;
        protected readonly string _rowNumberColumnName;
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";


        public CaseReasonsUpload(ILog logger,
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
            var connectionString = _jobConfiguration.SourceConnectionString;
            var tableName = _jobConfiguration.SourceTable;
            var synctime = DateTime.UtcNow.ToString(DateTimeFormat);
            var nonSyncCount = GetNonSyncRowCount();

            while (nonSyncCount > 0)
            {
                using (var sqlConn = new SqlConnection(connectionString))
                {
                    var sqlQuery = $"SELECT top {_batchSize} Id, Main_Reason__c, Sub_Reason__c, Uncertain_main_sub__c  FROM {tableName} (nolock) WHERE ETLSyncTime IS NULL";

                    using (var cmd = new SqlCommand(sqlQuery, sqlConn))
                    {
                        var table = new DataTable();
                        var ds = new SqlDataAdapter(cmd);

                        ds.Fill(table);

                        var salesforceUploader = _salesforceUploaderFactory.CreateUploader(_jobConfiguration.SalesforceObject);
                        var results = salesforceUploader.GetResults(table).ToList();

                        string updateQuery;

                        var updatedIds = results.Where(r => r.Success).Select(c => c.Id).ToList();
                        var errors = results.Where(r => !r.Success).ToList();

                       if (errors.Count > 0)
                        {
                            var sb = new StringBuilder();
                            errors.ForEach(e => sb.AppendLine(e.ToString()));
                            _logger.Error($"Errors occured for some cases while pushing the main and sub reasons \n: {sb}");
                        }

                        if (errors.Count == nonSyncCount && updatedIds.Count == 0)
                        {
                            break;
                        }

                        if (updatedIds.Count > 0)
                        {
                            _logger.Info($"Main and Sub reasons were pushed successfully for {updatedIds.Count}");
                            updateQuery = $"UPDATE {tableName} SET ETLSyncTime = '{synctime}' WHERE Id in ({string.Join(",", updatedIds.Select(c => $"'{c}'").ToList())})";
                        }
                        else
                        {
                            updateQuery = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(updateQuery))
                        {
                            _logger.Info($"Running this query to update ETLSyncTime: {updateQuery}");
                            SqlServerUtils.ExecuteCommandReturnNone(updateQuery, connectionString, true);
                        }
                    }
                }
            }

            await Task.CompletedTask;
        }

        private int GetNonSyncRowCount()
        {
            int count;
            var sqlTable = _jobConfiguration.SourceTable;
            var sqlConnectionName = _jobConfiguration.SourceConnectionString;
            var rowCount = $"SELECT COUNT(*) FROM {sqlTable} (nolock) WHERE ETLSyncTime IS NULL";

            try
            {
                count = SqlServerUtils.ExecuteCommandReturnSingle<int>(rowCount, sqlConnectionName);
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
