using System.Collections.Generic;
using System.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public class SalesforceUploadConfiguration : ISalesforceUploadConfiguration
    {
        public SalesforceUploadConfiguration(SalesforceUploadConfigurationSection config, ConnectionStringSettingsCollection connectionStrings)
        {
            Username = config.Username;
            Password = config.Password;
            SecurityToken = config.SecurityToken;
            LoginUrl = config.LoginUrl;
            BatchSize = config.BatchSize;
            RowNumberColumnName = config.RowNumberColumnName;

            Jobs = new Dictionary<string, ISalesforceUploadJobConfiguration>();

            foreach (var jobConfigObject in config.Instances)
            {
                if (jobConfigObject is SalesforceUploadJobElement jobConfig)
                {
                    var job = new SalesforceUploadJobConfiguration(jobConfig, connectionStrings);
                    Jobs.Add(job.Name.ToUpperInvariant(), job);
                }
            }
        }

        public IDictionary<string, ISalesforceUploadJobConfiguration> Jobs { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string SecurityToken { get; private set; }

        public string LoginUrl { get; private set; }

        public string RowNumberColumnName { get; private set; }

        public int BatchSize { get; private set; }

        public ISalesforceUploadJobConfiguration GetJobConfig(string jobName)
        {
            Jobs.TryGetValue(jobName.ToUpperInvariant(), out ISalesforceUploadJobConfiguration jobConfig);
            return jobConfig;
        }

        public override string ToString()
        {
            return $"LoginUrl: {LoginUrl}\nUsername: {Username}";
        }
    }
}
