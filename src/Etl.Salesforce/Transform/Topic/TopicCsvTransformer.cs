using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Topic
{
    public sealed class TopicCsvTransformer: CsvToDataTableTransformer<Model.Topic>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<TopicMap>);
        }
    }
}
