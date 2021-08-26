using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.WebActivity
{
    public sealed class WebActivityCsvTransformer: CsvToDataTableTransformer<Model.WebActivity>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<WebActivityMap>);
        }
    }
}
