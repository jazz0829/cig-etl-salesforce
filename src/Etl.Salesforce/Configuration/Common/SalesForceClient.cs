using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Configuration.Upload;
using System.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration.Common
{
	public class SalesForceClient : ISalesForceClient
	{
		private readonly ISalesforceConfiguration Configuration = new SalesforceConfiguration(ConfigurationManager.GetSection("salesforceConfiguration")
			as SalesforceConfigurationSection, ConfigurationManager.ConnectionStrings);

		private readonly ISalesforceUploadConfiguration UploadConfiguration = new SalesforceUploadConfiguration(ConfigurationManager.GetSection("salesforceUploadConfiguration")
			as SalesforceUploadConfigurationSection, ConfigurationManager.ConnectionStrings);

		public BulkApiClient GetBulkApiClient()
		{
			return new BulkApiClient(Configuration.Username, $"{Configuration.Password}{Configuration.SecurityToken}", Configuration.LoginUrl);
		}

		public BulkUploadApiClient GetSalesforceUploadApiClient()
		{
			return new BulkUploadApiClient(UploadConfiguration.Username, $"{UploadConfiguration.Password}{UploadConfiguration.SecurityToken}", UploadConfiguration.LoginUrl);
		}

		public SoapApiClient GetSalesforceSoapClient()
		{
			return new SoapApiClient(Configuration.Username, $"{Configuration.Password}{Configuration.SecurityToken}", Configuration.LoginUrl);
		}
	}
}
