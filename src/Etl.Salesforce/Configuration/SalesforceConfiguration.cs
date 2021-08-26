using System.Collections.Generic;
using System.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public class SalesforceConfiguration : ISalesforceConfiguration
    {
        public SalesforceConfiguration(SalesforceConfigurationSection config, ConnectionStringSettingsCollection connectionStrings)
        {
            Username = config.Username;
            Password = config.Password;
            SecurityToken = config.SecurityToken;
            LoginUrl = config.LoginUrl;

            Jobs = new Dictionary<string, ISalesforceJobConfiguration>();

            foreach (var jobConfigObject in config.Instances)
            {
                if (jobConfigObject is SalesforceJobElement jobConfig)
                {
                    var job = new SalesforceJobConfiguration(jobConfig, connectionStrings);
                    Jobs.Add(job.Name.ToUpperInvariant(), job);
                }
            }
        }

        public IDictionary<string, ISalesforceJobConfiguration> Jobs { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string SecurityToken { get; private set; }

        public string LoginUrl { get; private set; }

        public ISalesforceJobConfiguration GetJobConfig(string jobName)
        {
            ISalesforceJobConfiguration jobConfig;
            Jobs.TryGetValue(jobName.ToUpperInvariant(), out jobConfig);
            return jobConfig;
        }

        public override string ToString()
        {
            return $"LoginUrl: {LoginUrl}\nUsername: {Username}";
        }
    }
}
