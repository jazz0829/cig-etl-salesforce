using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Amazon.Kinesis;
using Eol.Cig.Etl.Kinesis.Producer;
using Eol.Cig.Etl.Salesforce.Configuration;
using Eol.Cig.Etl.Salesforce.Configuration.Common;
using Eol.Cig.Etl.Salesforce.Extract;
using Eol.Cig.Etl.Salesforce.Load;
using Eol.Cig.Etl.Salesforce.Transform;
using Eol.Cig.Etl.Shared.Load;
using log4net;
using Task = System.Threading.Tasks.Task;

namespace Eol.Cig.Etl.Salesforce.Jobs.Export
{
    public class KnowledgeArticleVersionExport : ExportJob
    {
        public static readonly string Name = nameof(KnowledgeArticleVersionExport);
        private readonly IAmazonKinesis _kinesis;
        protected readonly ISalesForceClient _salesForceClient;

		public KnowledgeArticleVersionExport(ILog logger, IAwsConfiguration awsConfiguration,
            ISalesforceConfiguration jobConfiguration,
            ISalesforceObjectTransformerFactory objectTransformerFactory,
            ISalesforceExtractorFactory salesforceExtractorFactory,
            ISqlServerUploaderFactory sqlServerUploaderFactory)
            : base(logger, awsConfiguration, jobConfiguration, objectTransformerFactory, salesforceExtractorFactory, sqlServerUploaderFactory)
        {
            _kinesis = new AmazonKinesisClient(awsConfiguration.AwsAccessKeyId, awsConfiguration.AwsSecretAccessKey, Amazon.RegionEndpoint.EUWest1);
            _salesForceClient = new SalesForceClient();
		}

        public override async Task Execute()
        {
            var dataLoader = _sqlServerUploaderFactory.Create(GetName().ToUpperInvariant()) as ISalesforceSqlServerUploader;
            var lastModifiedDate = dataLoader.GetLastModifiedDate(_jobConfiguration.SalesforceObject);
            var queryTimeBounds = GetTimeBoundBatches(lastModifiedDate);

            for (var i = 0; i < queryTimeBounds.Count; i++)
            {
                var queryTimeBound = queryTimeBounds[i];
                var lastTimeBound = i == queryTimeBounds.Count - 1;
                // Extract
                _logger.Info($"Exporting {GetName()}. Time bound: {queryTimeBound.Item1.ToSalesforceDateTimeFormat()} - {queryTimeBound.Item2.ToSalesforceDateTimeFormat()}");

                // There is no LastModifiedDate in the Topic object so let's use the CreatedDate field instead
                var results = _salesforceExtractorFactory.CreateSoapExtractor(_jobConfiguration.SalesforceObject, "CreatedDate", queryTimeBound, _salesForceClient.GetSalesforceSoapClient());
                var insertTime = DateTime.UtcNow;
                if (results.Count > 0)
                {
                    var dataTable = ConvertToDataTable(results, insertTime);

                    dataLoader.Upload(dataTable, insertTime, _jobConfiguration.SalesforceObject, lastTimeBound);

                    if (_awsConfiguration.IsStreamingEnabled)
                    {
                        var kinesisProducer = new KinesisWriter(_logger, _kinesis, _awsConfiguration.AwsKinesisStreamName, _awsConfiguration.S3Prefix, GetS3Name());

                        kinesisProducer.IngestData(dataTable);
                    }

                }
                else
                {
                    _logger.InfoFormat("There were no results.");
                }
            }

            await Task.CompletedTask;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data, DateTime insertTime)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            var etlInsertTimeColumn = new DataColumn("EtlInsertTime", typeof(DateTime))
            {
                AllowDBNull = false,
                DateTimeMode = DataSetDateTime.Utc,
                DefaultValue = insertTime
            };

            table.Columns.Add(etlInsertTimeColumn);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}
