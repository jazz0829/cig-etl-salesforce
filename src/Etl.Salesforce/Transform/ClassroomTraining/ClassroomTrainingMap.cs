using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.ClassroomTraining
{
	public class ClassroomTrainingTransformerMap : CsvClassMap<Model.ClassroomTraining>
	{
		public ClassroomTrainingTransformerMap()
		{
			AutoMap();
			Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.LastReferencedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.LastViewedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
		}
	}
}
