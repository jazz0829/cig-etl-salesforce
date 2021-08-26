using System;
using System.Globalization;
using CsvHelper.TypeConversion;

namespace Eol.Cig.Etl.Salesforce.Transform
{
    public class StringToDateConverter: ITypeConverter
    {
        public string ConvertToString(TypeConverterOptions options, object value)
        {
            return value.ToString();
        }

        public object ConvertFromString(TypeConverterOptions options, string value)
        {
            DateTime dateTime;
            if (DateTime.TryParse(value, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out dateTime))
            {
                return dateTime.Date;
            }

            return (DateTime?)null;
        }

        public bool CanConvertFrom(Type type)
        {
            return type == typeof (string);
        }

        public bool CanConvertTo(Type type)
        {
            return type == typeof (DateTime);
        }
    }
}