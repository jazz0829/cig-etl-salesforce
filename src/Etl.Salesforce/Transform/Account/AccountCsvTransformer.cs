using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Account
{
    public sealed class AccountCsvTransformer : CsvToDataTableTransformer<Model.Account>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<AccountMap>);
        }
    }
}
