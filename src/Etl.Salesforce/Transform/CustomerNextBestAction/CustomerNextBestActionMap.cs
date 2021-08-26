using CsvHelper.Configuration;
using System.Globalization;

namespace Eol.Cig.Etl.Salesforce.Transform.CustomerNextBestAction
{
    public sealed class CustomerNextBestActionMap: CsvClassMap<Model.CustomerNextBestAction>
    {
        public CustomerNextBestActionMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
