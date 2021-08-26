using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.TopicAssignment
{
    public class TopicAssignmentMap: CsvClassMap<Model.TopicAssignment>
    {
        public TopicAssignmentMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
