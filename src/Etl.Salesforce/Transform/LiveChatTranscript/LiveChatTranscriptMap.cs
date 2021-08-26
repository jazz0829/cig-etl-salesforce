using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscript
{
    public class LiveChatTranscriptMap : CsvClassMap<Model.LiveChatTranscript>
    {
        public LiveChatTranscriptMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.StartTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.EndTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.RequestTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
