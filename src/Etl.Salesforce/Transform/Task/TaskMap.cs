using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.Task
{
	public class TaskMap : CsvClassMap<Model.Task>
	{
		public TaskMap()
		{
			AutoMap();
			Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.Action_follow_up_date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal); ;
			Map(m => m.ActivityDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.Completion_Date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal); ;
			Map(m => m.RecurrenceEndDateOnly).TypeConverterOption(DateTimeStyles.AdjustToUniversal); ;
			Map(m => m.RecurrenceStartDateOnly).TypeConverterOption(DateTimeStyles.AdjustToUniversal); ;
			Map(m => m.ReminderDateTime).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
			Map(m => m.SystemModstamp).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
		}
	}
}
