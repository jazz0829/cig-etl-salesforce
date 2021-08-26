using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscriptEvent
{
    public sealed class LiveChatTranscriptEventCsvTransformer : CsvToDataTableTransformer<Model.LiveChatTranscriptEvent>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveChatTranscriptEventMap>);
        }
    }
}
