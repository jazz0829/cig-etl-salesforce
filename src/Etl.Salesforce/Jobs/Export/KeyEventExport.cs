﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Eol.Cig.Etl.Kinesis.Producer;
using Eol.Cig.Etl.Salesforce.Configuration;
using Eol.Cig.Etl.Salesforce.Configuration.Common;
using Eol.Cig.Etl.Salesforce.Extract;
using Eol.Cig.Etl.Salesforce.Transform;
using Eol.Cig.Etl.Shared.Load;
using log4net;
using Eol.Cig.Etl.Salesforce.Load;

namespace Eol.Cig.Etl.Salesforce.Jobs.Export
{
	public class KeyEventExport : ExportJob
	{
		public static readonly string Name = nameof(KeyEventExport);
		private readonly IAmazonKinesis _kinesis;
		protected readonly ISalesForceClient _salesForceClient;

		public KeyEventExport(ILog logger, IAwsConfiguration awsConfiguration,
			ISalesforceConfiguration jobConfiguration,
			ISalesforceObjectTransformerFactory objectTransformerFactory,
			ISalesforceExtractorFactory salesforceExtractorFactory,
			ISqlServerUploaderFactory sqlServerUploaderFactory)
			: base(logger, awsConfiguration, jobConfiguration, objectTransformerFactory, salesforceExtractorFactory,
				sqlServerUploaderFactory)
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
				var salesforceExtractor = _salesforceExtractorFactory.CreateExtractor(_jobConfiguration.SalesforceObject, "CreatedDate", queryTimeBound, _salesForceClient.GetBulkApiClient());
				var results = salesforceExtractor.GetResults().ToList();

				for (var j = 0; j < results.Count; j++)
				{
					var result = results[j];
					var lastResult = j == results.Count - 1;

					if (result == "Records not found for this query")
					{
						_logger.InfoFormat("There were no results.");
					}
					else
					{
						//If there is more than one batch here, do we get the result ordered?
						//We need to keep in mind that last modified date query makes sense if al results are imported at once.
						//Multiple results would be an issue.
						//This should be solved either by inspecting the salesforce job
						//Or by expanding the transaction to span all results.
						//Maby GetResults can buffer?? That might be problematic in bigger data sets.

						var insertTime = DateTime.UtcNow;
						
						//Transform
						var transformer = _objectTransformerFactory.CreateTransformer(_jobConfiguration.SalesforceObject);
						var transformedData = transformer.Transform(result, insertTime);
						
						//Load
						_logger.Info($"Transformed row count: {transformedData.Rows.Count}");

						dataLoader.Upload(transformedData, insertTime, _jobConfiguration.SalesforceObject, lastTimeBound && lastResult);

						if (_awsConfiguration.IsStreamingEnabled)
						{
							var kinesisProducer = new KinesisWriter(_logger, _kinesis, _awsConfiguration.AwsKinesisStreamName, _awsConfiguration.S3Prefix, GetS3Name());

							kinesisProducer.IngestData(transformedData);
						}
					}
				}
			}

			await Task.CompletedTask;
		}

		public override string GetName()
		{
			return Name;
		}
	}
}
