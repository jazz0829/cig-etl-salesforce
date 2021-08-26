using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.IndividualEmailResult
{
    public sealed class IndividualEmailResultCsvTransformer : CsvToDataTableTransformer<Model.IndividualEmailResult>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<IndividualEmailResultMap>);
        }
    }
}
