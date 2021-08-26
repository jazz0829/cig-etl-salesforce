using Eol.Cig.Etl.Salesforce.Client;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
	public interface ISalesForceClient
	{
		BulkApiClient GetBulkApiClient();

		BulkUploadApiClient GetSalesforceUploadApiClient();

		SoapApiClient GetSalesforceSoapClient();
	}
}
