using System;
using System.Collections.Generic;
using Eol.Cig.Etl.Salesforce.Client;

namespace Eol.Cig.Etl.Salesforce.Extract
{
	public interface ISalesforceExtractorFactory
	{
		ISalesforceExtractor CreateExtractor(string objectName, Tuple<DateTime, DateTime> timePeriod, BulkApiClient apiClient);

		ISalesforceExtractor CreateExtractor(string objectName, string criteriaField,
			Tuple<DateTime, DateTime> timePeriod, BulkApiClient apiClient);
		List<Model.KnowledgeArticleVersion> CreateSoapExtractor(string objectName, string criteriaField, Tuple<DateTime, DateTime> timePeriod, SoapApiClient soapApiClient);
	}
}