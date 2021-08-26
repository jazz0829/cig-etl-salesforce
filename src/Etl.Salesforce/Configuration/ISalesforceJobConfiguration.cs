using Eol.Cig.Etl.Shared.Configuration;

namespace Eol.Cig.Etl.Salesforce.Configuration
{
    public interface ISalesforceJobConfiguration: ISqlJobConfiguration
    {
        string SalesforceObject { get; }
    }
}