using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveAgentSession
{
    public class LiveAgentSessionMap : CsvClassMap<Model.LiveAgentSession>
    {
        public LiveAgentSessionMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LoginTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LogoutTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
