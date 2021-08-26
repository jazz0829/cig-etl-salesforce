using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Configuration.Common;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Upload
{
    public class SalesforceUploaderFactory: ISalesforceUploaderFactory
    {
        private readonly SalesForceClient _salesForceClient;
        private readonly ILog _logger;

        public SalesforceUploaderFactory(SalesForceClient salesForceClient, ILog logger)
        {
	        _salesForceClient = salesForceClient;
            _logger = logger;
        }

        public ISalesforceUploader CreateUploader(string objectName, JobOperation operation = JobOperation.Update)
        {
            _logger.InfoFormat("Creating uploader for object: {0}", objectName);

            var queryJobRequest = new CreateJobRequest
            {
                Operation = operation,
                ContentType = JobContentType.Csv,
                Object = objectName
            };
            var salesforceBatchJob = new SalesforceBulkUploader(_salesForceClient, _logger, queryJobRequest);
            return salesforceBatchJob;
        }
    }
}
