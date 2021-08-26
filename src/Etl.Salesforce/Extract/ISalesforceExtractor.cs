using System.Collections.Generic;

namespace Eol.Cig.Etl.Salesforce.Extract
{
    public interface ISalesforceExtractor
    {
        IEnumerable<string> GetResults();
    }
}