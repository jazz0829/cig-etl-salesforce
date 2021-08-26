using Eol.Cig.Etl.Salesforce.Common;
using System.Collections.Generic;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Upload
{
    public interface ISalesforceUploader
    {
        IEnumerable<AccountUploadResult> GetResults(DataTable table);
    }
}
