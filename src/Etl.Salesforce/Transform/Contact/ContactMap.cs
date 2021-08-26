using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.Contact
{
    public sealed class ContactMap: CsvClassMap<Model.Contact>
    {
        public ContactMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastActivityDate).TypeConverter<StringToDateConverter>();
            Map(m => m.LastCURequestDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastCUUpdateDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastViewedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Enddate__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.Startdate__c).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
