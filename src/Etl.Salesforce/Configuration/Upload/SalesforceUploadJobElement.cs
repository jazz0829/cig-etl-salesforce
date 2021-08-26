using System.Configuration;
using Eol.Cig.Etl.Shared.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public class SalesforceUploadJobElement : JobConfigurationElementBase, IConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => base["name"];

        [ConfigurationProperty("sourceConnectionStringName", IsRequired = true)]
        public string SourceConnectionStringName => base["sourceConnectionStringName"];

        [ConfigurationProperty("sourceTable", IsRequired = true)]
        public string SourceTable => base["sourceTable"];

        [ConfigurationProperty("salesforceObject", IsRequired = true)]
        public string SalesforceObject => base["salesforceObject"];
    }
}
