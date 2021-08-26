using System;
using System.Data;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform
{
    public static class CsvTransformerHelper
    {
        public static DataTable Transform(string csvData, DateTime insertTime, Func<CsvReader, DateTime, DataTable> transformer)
        {
            using (var stringReader = new StringReader(csvData))
            {
                var csvConfiguration = new CsvConfiguration { HasHeaderRecord = true };
                using (var csvReader = new CsvReader(stringReader, csvConfiguration))
                {
                    return transformer(csvReader, insertTime);
                }
            }
        }
    }
}