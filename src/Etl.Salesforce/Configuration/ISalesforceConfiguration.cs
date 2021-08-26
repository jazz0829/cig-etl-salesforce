using Eol.Cig.Etl.Salesforce.Configuration.Common;
using System.Collections.Generic;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public interface ISalesforceConfiguration: ISalesforceSettings
    {
        IDictionary<string, ISalesforceJobConfiguration> Jobs { get; }
        ISalesforceJobConfiguration GetJobConfig(string jobName);
    }
}