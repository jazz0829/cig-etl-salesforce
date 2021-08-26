using Eol.Cig.Etl.Salesforce.Configuration.Common;
using System.Collections.Generic;

namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public interface ISalesforceUploadConfiguration: ISalesforceSettings
    {
        string RowNumberColumnName { get; }
        int BatchSize { get; }
        IDictionary<string, ISalesforceUploadJobConfiguration> Jobs { get; }
        ISalesforceUploadJobConfiguration GetJobConfig(string jobName);
    }
}