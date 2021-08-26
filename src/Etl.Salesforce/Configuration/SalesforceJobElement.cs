using System.Configuration;
using Eol.Cig.Etl.Shared.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public class SalesforceJobElement: JobConfigurationElementBase, IConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => base["name"];

        [ConfigurationProperty("sqlCsvDelimiter", IsRequired = false)]
        public string SqlCsvDelimiter => base["sqlCsvDelimiter"];

        [ConfigurationProperty("destinationConnectionStringName", IsRequired = true)]
        public string DestinationConnectionStringName => base["destinationConnectionStringName"];

        [ConfigurationProperty("destinationTable", IsRequired = true)]
        public string DestinationTable => base["destinationTable"];

        [ConfigurationProperty("salesforceObject", IsRequired = true)]
        public string SalesforceObject => base["salesforceObject"];
    }
}
