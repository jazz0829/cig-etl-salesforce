using System.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public class SalesforceConfigurationSection: ConfigurationSection
    {
        [ConfigurationProperty("securityToken", IsKey = true, IsRequired = true)]
        public string SecurityToken => (string)this["securityToken"];

        [ConfigurationProperty("loginUrl", IsKey = true, IsRequired = true)]
        public string LoginUrl => (string)this["loginUrl"];

        [ConfigurationProperty("username", IsKey = true, IsRequired = true)]
        public string Username => (string)this["username"];

        [ConfigurationProperty("password", IsKey = true, IsRequired = true)]
        public string Password => (string)this["password"];

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public SalesforceJobConfigurationCollection Instances
        {
            get => (SalesforceJobConfigurationCollection)this[""];
            set => this[""] = value;
        }
    }
}
