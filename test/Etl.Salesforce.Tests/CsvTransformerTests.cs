using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Eol.Cig.Etl.Salesforce.Transform.Case;
using Xunit;

namespace Eol.Cig.Etl.Salesforce.Tests
{
    public class CsvTransformerTests
    {
        // Test that all dates are translated to UTC. They are in utc in the file and must be translated into DateTime.Kind = Utc
        // For any additional mapping, test needs to be here
        private string LoadCsvTestData(string objectName)
        {
            var currentDir = Environment.CurrentDirectory;
            var filePath = Path.Combine(currentDir, "CsvTestData", $"{objectName}.csv");
            var data = File.ReadAllText(filePath);
            return data;
        }


        [Fact]
        public void Given_CsvData_And_CaseCsvTransformer_When_Transform_Is_Run_Then_Result_Dates_NeedTo_Be_Of_Kind_Utc()
        {
            string csvData = LoadCsvTestData("Case");
            var transformer = new CaseCsvTransformer();
            var insertTime = DateTime.UtcNow;
            var dataTableResult = transformer.Transform(csvData, insertTime);
            var nonUtcColumns = GetNoNonUtcDateColumns(dataTableResult);

            Assert.Empty(nonUtcColumns);
        }

        private List<string> GetNoNonUtcDateColumns(DataTable dataTableResult)
        {
            var nonUtcDateColumns = new List<string>();

            foreach (var row in dataTableResult.Rows)
                foreach (var col in dataTableResult.Columns)
                {
                    var column = col as DataColumn;
                    if (column.DataType == typeof(DateTime) && column.DateTimeMode != DataSetDateTime.Utc)
                    {
                        var dataRow = row as DataRow;
                        var value = (DateTime)dataRow[column.ColumnName];

                        if (value.Kind != DateTimeKind.Utc &&
                            !nonUtcDateColumns.Exists(c => c.Equals(column.ColumnName, StringComparison.OrdinalIgnoreCase)))
                        {
                            nonUtcDateColumns.Add(column.ColumnName);
                        }
                    }
                }

            return nonUtcDateColumns;
        }
    }
}
