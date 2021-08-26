using CsvHelper;
using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Common;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Eol.Cig.Etl.Salesforce.Configuration.Common;

namespace Eol.Cig.Etl.Salesforce.Upload
{
    public class SalesforceBulkUploader : ISalesforceUploader
    {
        private readonly BulkUploadApiClient _apiClient;
        private readonly ILog _logger;
        private readonly CreateJobRequest _queryJobRequest;

        public SalesforceBulkUploader(SalesForceClient salesForceClient, ILog logger, CreateJobRequest queryJobRequest)
        {
	        _apiClient = salesForceClient.GetSalesforceUploadApiClient();
			_logger = logger;
            _queryJobRequest = queryJobRequest;
        }

        private string ConvertToCsv(DataTable table)
        {
            var sb = new StringBuilder();

            IEnumerable<string> columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in table.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        public IEnumerable<AccountUploadResult> GetResults(DataTable table)
        {
            var queryJob = _apiClient.CreateJob(_queryJobRequest);

            _logger.InfoFormat("Created Salesforce query job. Job id: {0}", queryJob.Id);

            var csvData = ConvertToCsv(table);

            var batchQuery = new CreateBatchRequest
            {
                JobId = queryJob.Id,
                BatchContents = csvData,
                ContentType = BatchContentType.Csv
            };

            var queryBatch = _apiClient.CreateBatch(batchQuery);
            _logger.InfoFormat("Created Salesforce batch. Batch id: {0}", queryBatch.Id);

            _apiClient.CloseJob(queryJob.Id);

            _logger.InfoFormat("Waiting for Salesforce job to complete. Job Id: {0}", queryJob.Id);
            
            _apiClient.GetCompletedJob(queryJob.Id);

            _logger.InfoFormat("Salesforce job completed. Job Id: {0}", queryJob.Id);

            var queryResultList = _apiClient.GetBatchResults(queryBatch.JobId, queryBatch.Id);

            using (var sr = new StringReader(queryResultList))
            {
                var csv = new CsvReader(sr);
                csv.Configuration.RegisterClassMap<AccountUploadResultMap>();
                var uploadResultList = csv.GetRecords<AccountUploadResult>().ToList();
                var i = 0;

                uploadResultList.ForEach(c => c.RowNumber = i++);

                return uploadResultList;

            }
        }
    }
}
