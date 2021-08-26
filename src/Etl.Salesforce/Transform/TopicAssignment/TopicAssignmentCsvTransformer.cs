using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.TopicAssignment
{
    public sealed class TopicAssignmentCsvTransformer:CsvToDataTableTransformer<Model.TopicAssignment>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<TopicAssignmentMap>);
        }
    }
}
