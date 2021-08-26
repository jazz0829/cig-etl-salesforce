using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.SkillUser
{
    public class SkillUserMap : CsvClassMap<Model.SkillUser>
    {
        public SkillUserMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
