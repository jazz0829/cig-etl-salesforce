using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.KeyEvent
{
    public class KeyEventMap : CsvClassMap<Model.KeyEvent>
    {
        public KeyEventMap()
        {
            AutoMap();
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
