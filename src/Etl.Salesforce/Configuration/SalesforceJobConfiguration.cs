using System.Configuration;
using System.Data.SqlClient;
using Eol.Cig.Etl.Shared.Extensions;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public class SalesforceJobConfiguration: ISalesforceJobConfiguration
    {
        private readonly SqlConnectionStringBuilder builder;

        public SalesforceJobConfiguration(SalesforceJobElement jobSettings,
            ConnectionStringSettingsCollection connectionStringSettings)
        {
            Name = jobSettings.GetStringOrThrow("name");

            DestinationConnectionStringName = jobSettings.GetStringOrThrow("destinationConnectionStringName");
            DestinationConnectionString = connectionStringSettings.GetConnectionStringOrThrow(DestinationConnectionStringName);
            DestinationTable = jobSettings.GetStringOrThrow("destinationTable");
            SalesforceObject = jobSettings.GetStringOrThrow("salesforceObject");

            builder = new SqlConnectionStringBuilder(DestinationConnectionString);
        }

        public char SqlCsvDelimiter => ',';
        public string DestinationConnectionStringName { get; }
        public string DestinationConnectionString { get; set; }
        public string Name { get; }
        public string ServerName => builder.DataSource;
        public string DatabaseName => builder.InitialCatalog;
        public string DestinationTable { get; }
        public string SalesforceObject { get; }
    }
}
