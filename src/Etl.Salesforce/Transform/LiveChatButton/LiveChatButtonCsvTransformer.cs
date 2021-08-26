using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatButton
{
    public sealed class LiveChatButtonCsvTransformer : CsvToDataTableTransformer<Model.LiveChatButton>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveChatButtonMap>);
        }
    }
}
