using System.Collections.Generic;
using System.Linq;
using Eol.Cig.Etl.Salesforce.Client;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Extract
{
    public class SalesforceBulkExtractor: ISalesforceExtractor
    {
        private readonly BulkApiClient _apiClient;
        private readonly ILog _logger;
        private readonly CreateJobRequest _queryJobRequest;
        private readonly string _query;

        public SalesforceBulkExtractor(BulkApiClient apiClient, ILog logger, CreateJobRequest queryJobRequest, string query)
        {
            _apiClient = apiClient;
            _logger = logger;
            _queryJobRequest = queryJobRequest;
            _query = query;
        }

        public IEnumerable<string> GetResults()
        {
            var queryJob = _apiClient.CreateJob(_queryJobRequest);

            _logger.InfoFormat("Created Salesforce query job. Job id: {0}", queryJob.Id);

            var batchQuery = new CreateBatchRequest
            {
                JobId = queryJob.Id,
                BatchContents = _query,
                ContentType = BatchContentType.Csv
            };

            var queryBatch = _apiClient.CreateBatch(batchQuery);
            _logger.InfoFormat("Created Salesforce batch. Batch id: {0}", queryBatch.Id);

            //When done, close the job (This is ok since we only add one batch to the job)
            _apiClient.CloseJob(queryJob.Id);

            _logger.InfoFormat("Waiting for Salesforce job to complete. Job Id: {0}", queryJob.Id);
            // This can take some time. We are waiting for salesforce to build the job.
            _apiClient.GetCompletedJob(queryJob.Id);

            _logger.InfoFormat("Salesforce job completed. Job Id: {0}", queryJob.Id);

            var queryResultList = _apiClient.GetBatchResults(queryBatch.JobId, queryBatch.Id);
            var queryResultIds = _apiClient.GetResultIds(queryResultList);

            return queryResultIds.Select(queryResultId => _apiClient.GetBatchResult(queryBatch.JobId, queryBatch.Id, queryResultId));
        }
    }
}