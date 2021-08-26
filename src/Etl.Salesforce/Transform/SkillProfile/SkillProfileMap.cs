using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.SkillProfile
{
    public class SkillProfileMap : CsvClassMap<Model.SkillProfile>
    {
        public SkillProfileMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
