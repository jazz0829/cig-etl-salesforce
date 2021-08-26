using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform
{
    public interface ISalesforceObjectTransformer
    {
        DataTable Transform(string data, DateTime insertTime);
    }
}