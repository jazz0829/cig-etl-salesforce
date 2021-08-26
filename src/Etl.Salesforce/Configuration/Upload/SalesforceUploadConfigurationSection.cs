using System.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public class SalesforceUploadConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("securityToken", IsKey = true, IsRequired = true)]
        public string SecurityToken => (string)this["securityToken"];

        [ConfigurationProperty("loginUrl", IsKey = true, IsRequired = true)]
        public string LoginUrl => (string)this["loginUrl"];

        [ConfigurationProperty("username", IsKey = true, IsRequired = true)]
        public string Username => (string)this["username"];

        [ConfigurationProperty("password", IsKey = true, IsRequired = true)]
        public string Password => (string)this["password"];

        [ConfigurationProperty("batchSize", IsKey = true, IsRequired = true)]
        public int BatchSize => (int)this["batchSize"];

        [ConfigurationProperty("rowNumberColumnName", IsKey = true, IsRequired = true)]
        public string RowNumberColumnName => (string)this["rowNumberColumnName"];

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public SalesforceUploadJobConfigurationCollection Instances
        {
            get => (SalesforceUploadJobConfigurationCollection)this[""];
            set => this[""] = value;
        }
    }
}
