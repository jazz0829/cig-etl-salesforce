using System.Configuration;
using System.Data.SqlClient;
using Eol.Cig.Etl.Shared.Extensions;

namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public class SalesforceUploadJobConfiguration : ISalesforceUploadJobConfiguration
    {
        private readonly SqlConnectionStringBuilder builder;

        public SalesforceUploadJobConfiguration(SalesforceUploadJobElement jobSettings,
            ConnectionStringSettingsCollection connectionStringSettings)
        {
            Name = jobSettings.GetStringOrThrow("name");

            SourceConnectionStringName = jobSettings.GetStringOrThrow("sourceConnectionStringName");
            SourceConnectionString = connectionStringSettings.GetConnectionStringOrThrow(SourceConnectionStringName);
            SourceTable = jobSettings.GetStringOrThrow("sourceTable");
            SalesforceObject = jobSettings.GetStringOrThrow("salesforceObject");

            builder = new SqlConnectionStringBuilder(SourceConnectionString);
        }
        public string SourceConnectionStringName { get; }
        public string SourceConnectionString { get; set; }
        public string Name { get; }
        public string ServerName => builder.DataSource;
        public string DatabaseName => builder.InitialCatalog;
        public string SourceTable { get; }
        public string SalesforceObject { get; }
    }
}
