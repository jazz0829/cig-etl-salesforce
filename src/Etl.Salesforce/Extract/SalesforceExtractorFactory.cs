using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Model;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Extract
{
    public class SalesforceExtractorFactory : ISalesforceExtractorFactory
    {
        private static readonly Dictionary<string, string> _salesforceObjectQueries = new Dictionary<string, string>();

        //Create a cache map of salesforce objects and queries.
        static SalesforceExtractorFactory()
        {
            var salesforceObjects = Assembly.GetAssembly(typeof(SalesforceObject)).GetTypes()
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            t.IsSubclassOf(typeof(SalesforceObject))).ToList();
            foreach (var salesforceObject in salesforceObjects)
            {
                var objectName = (string)salesforceObject.GetField("ObjectName").GetValue(null);
                var query = (string)salesforceObject.GetField("Query").GetValue(null);
                _salesforceObjectQueries.Add(objectName, query);
            }
        }

        private readonly ILog _logger;

        public SalesforceExtractorFactory(ILog logger)
        {
            _logger = logger;
        }

        private string FindSalesforceQuery(string objectName)
        {
            var query = _salesforceObjectQueries[objectName.ToUpper(CultureInfo.InvariantCulture)];

            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"Object {objectName} is not implemented!");
            }

            return query;
        }

        public ISalesforceExtractor CreateExtractor(string objectName, Tuple<DateTime, DateTime> timePeriod, BulkApiClient apiClient)
        {
            var query = FindSalesforceQuery(objectName);
            var salesforceQuery = $@"{query} WHERE LastModifiedDate > {timePeriod.Item1.ToSalesforceDateTimeFormat()} AND LastModifiedDate <= {timePeriod.Item2.ToSalesforceDateTimeFormat()}";

            _logger.InfoFormat("Creating extractor with query: {0}", salesforceQuery);

            var queryJobRequest = CreateJobRequest.CreateQueryJob(objectName);
            var salesforceBatchJob = new SalesforceBulkExtractor(apiClient, _logger, queryJobRequest, salesforceQuery);
            return salesforceBatchJob;
        } 

        public ISalesforceExtractor CreateExtractor(string objectName, string criteriaField, Tuple<DateTime, DateTime> timePeriod, BulkApiClient apiClient)
        {
            var query = FindSalesforceQuery(objectName);
            var salesforceQuery = $@"{query} WHERE {criteriaField} > {timePeriod.Item1.ToSalesforceDateTimeFormat()} AND {criteriaField} <= {timePeriod.Item2.ToSalesforceDateTimeFormat()}";

            _logger.InfoFormat("Creating extractor with query: {0}", salesforceQuery);

            var queryJobRequest = CreateJobRequest.CreateQueryJob(objectName);
            var salesforceBatchJob = new SalesforceBulkExtractor(apiClient, _logger, queryJobRequest, salesforceQuery);
            return salesforceBatchJob;
        }


        public List<Model.KnowledgeArticleVersion> CreateSoapExtractor(string objectName, string criteriaField, Tuple<DateTime, DateTime> timePeriod, SoapApiClient soapApiClient)
        {
            var query = FindSalesforceQuery(objectName);
            var salesforceQuery = $@"{query} WHERE {criteriaField} > {timePeriod.Item1.ToSalesforceDateTimeFormat()} AND {criteriaField} <= {timePeriod.Item2.ToSalesforceDateTimeFormat()}";

            _logger.InfoFormat("Creating extractor with query: {0}", salesforceQuery);

            return ConvertToKnowledgeArticleVersion(soapApiClient.GetData(salesforceQuery));
        }

        private List<Model.KnowledgeArticleVersion> ConvertToKnowledgeArticleVersion(List<sObject> list)
        {
            var result = new List<Model.KnowledgeArticleVersion>();

            foreach (var item in list.Cast<Client.KnowledgeArticleVersion>())
            {
                result.Add(new Model.KnowledgeArticleVersion
                {
                    ArticleCreatedDate = item.ArticleCreatedDate,
                    ArticleNumber = item.ArticleNumber,
                    ArticleType = item.ArticleType,
                    CreatedDate = item.CreatedDate,
                    FirstPublishedDate = item.FirstPublishedDate,
                    Id = item.Id,
                    Language = item.Language,
                    LastPublishedDate = item.LastPublishedDate,
                    PublishStatus = item.PublishStatus,
                    SourceId = item.SourceId,
                    Summary = item.Summary,
                    Title = item.Title,
                    UrlName = item.UrlName
                });
            }

            return result;
        }

    }
}
