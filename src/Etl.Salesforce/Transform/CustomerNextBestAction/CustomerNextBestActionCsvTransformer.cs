using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.CustomerNextBestAction
{
    public sealed class CustomerNextBestActionCsvTransformer: CsvToDataTableTransformer<Model.CustomerNextBestAction>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<CustomerNextBestActionMap>);
        }
    }
}
