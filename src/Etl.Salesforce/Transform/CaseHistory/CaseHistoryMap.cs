using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.CaseHistory
{
	public class CaseHistoryMap : CsvClassMap<Model.CaseHistory>
	{
		public CaseHistoryMap()
		{
			AutoMap();
			Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
		}
	}
}
