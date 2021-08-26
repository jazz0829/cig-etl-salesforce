using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.Survey
{
    public class SurveyMap: CsvClassMap<Model.Survey>
    {
        public SurveyMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastActivityDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
