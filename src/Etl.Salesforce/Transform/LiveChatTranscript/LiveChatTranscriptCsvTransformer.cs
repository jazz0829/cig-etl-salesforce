using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscript
{
    public sealed class LiveChatTranscriptCsvTransformer : CsvToDataTableTransformer<Model.LiveChatTranscript>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveChatTranscriptMap>);
        }
    }
}
