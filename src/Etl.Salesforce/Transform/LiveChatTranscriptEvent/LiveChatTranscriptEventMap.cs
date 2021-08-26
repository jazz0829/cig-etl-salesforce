using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscriptEvent
{
    public class LiveChatTranscriptEventMap : CsvClassMap<Model.LiveChatTranscriptEvent>
    {
        public LiveChatTranscriptEventMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Time).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
