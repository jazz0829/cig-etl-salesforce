using Eol.Cig.Etl.Salesforce.Client;

namespace Eol.Cig.Etl.Salesforce.Upload
{
    public interface ISalesforceUploaderFactory
    {
        ISalesforceUploader CreateUploader(string objectName, JobOperation operation = JobOperation.Update);
    }
}
