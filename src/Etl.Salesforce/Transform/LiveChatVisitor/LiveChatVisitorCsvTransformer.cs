using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatVisitor
{
    public sealed class LiveChatVisitorCsvTransformer : CsvToDataTableTransformer<Model.LiveChatVisitor>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveChatVisitorMap>);
        }
    }
}
