using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatVisitor
{
    public class LiveChatVisitorMap : CsvClassMap<Model.LiveChatVisitor>
    {
        public LiveChatVisitorMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
