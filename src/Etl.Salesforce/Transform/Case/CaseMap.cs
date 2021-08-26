using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.Case
{
    public class CaseMap: CsvClassMap<Model.Case>
    {
        public CaseMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.ClosedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastReferencedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastViewedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Date_Time_Answered__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Enddate__c).TypeConverter<StringToDateConverter>();
            Map(m => m.Exact_Creation_Date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Planned_End_Date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Startdate__c).TypeConverter<StringToDateConverter>();
            Map(m => m.Start_Date__c).TypeConverter<StringToDateConverter>();
            Map(m => m.Date_Time_Reopened__c).TypeConverter<StringToDateConverter>();
            Map(m => m.Last_Public_Comment_Date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Action_follow_up_date__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.End_Stage_Check_up__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.End_Stage_Closing__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.End_Stage_New__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.End_Stage_Welcome__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Planned_go_live__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Stage_Closed__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
