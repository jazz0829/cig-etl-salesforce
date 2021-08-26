using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Case
{
    public sealed class CaseCsvTransformer : CsvToDataTableTransformer<Model.Case>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<CaseMap>);
        }
    }
}
