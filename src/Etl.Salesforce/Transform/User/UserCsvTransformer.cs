using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.User
{
    public sealed class UserCsvTransformer : CsvToDataTableTransformer<Model.User>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<UserMap>);
        }
    }
}
