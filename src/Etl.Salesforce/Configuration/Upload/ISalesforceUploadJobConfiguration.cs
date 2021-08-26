namespace Eol.Cig.Etl.Salesforce.Configuration.Upload
{
    public interface ISalesforceUploadJobConfiguration
    {
        string SalesforceObject { get; }
        string SourceTable { get; }
        string SourceConnectionString { get; }
    }
}