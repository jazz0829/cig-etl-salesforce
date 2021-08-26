using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Eol.Cig.Etl.Salesforce.Transform.IndividualEmailResult
{
    public class IndividualEmailResultMap : CsvClassMap<Model.IndividualEmailResult>
    {
        public IndividualEmailResultMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__DateBounced__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__DateOpened__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__DateSent__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__DateUnsubscribed__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__Tracking_As_Of__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.et4ae5__Tracking_As_Of__c).TypeConverter<StringToDateConverter>();
            Map(m => m.et4ae5__NumberOfTotalClicks__c).TypeConverter<DecimalConverter>();
            Map(m => m.et4ae5__NumberOfUniqueClicks__c).TypeConverter<DecimalConverter>();
        }
    }
}
