using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using StructureMap.TypeRules;

namespace Eol.Cig.Etl.Salesforce.Transform
{
    /// <summary>
    /// Takes all properties of <see cref="T"/> and builds a data table with same columns.
    /// Properties are statically mapped so any runntime additions are not taken into account.
    /// DateTime properties are mapped to Utc!
    /// </summary>
    /// <typeparam name="T">Type that will get mapped to DataTable</typeparam>
    public class CsvToDataTableTransformer<T> where T : class
    {
        private readonly Dictionary<string, Type> _columnTypeMapping = new Dictionary<string, Type>();
        private readonly List<PropertyInfo> _properties;
        private readonly string EtlInsertTime = "EtlInsertTime";

        protected CsvToDataTableTransformer()
        {
            _properties = typeof(T).GetProperties().ToList();
            foreach (var property in _properties)
            {
                _columnTypeMapping.Add(property.Name, property.PropertyType);
            }
        }

        private void ConfigureDataTableColumns(DataTable table)
        {
            foreach (var column in _columnTypeMapping.Keys)
            {
                var type = _columnTypeMapping[column];
                var isNullableType = type.IsNullable();
                if (isNullableType)
                {
                    type = Nullable.GetUnderlyingType(_columnTypeMapping[column]);
                }

                var dataColumn = new DataColumn(column, type)
                {
                    AllowDBNull = isNullableType
                };
                if (type == typeof(DateTime))
                {
                    dataColumn.DateTimeMode = DataSetDateTime.Utc;
                }

                table.Columns.Add(dataColumn);
            }

            var etlInsertTimeColumn = new DataColumn(EtlInsertTime, typeof(DateTime))
            {
                AllowDBNull = false,
                DateTimeMode = DataSetDateTime.Utc
            };

            table.Columns.Add(etlInsertTimeColumn);
        }

        public List<SqlBulkCopyColumnMapping> GetColumnMappings()
        {
            var mappings = _properties.Select(prop => new SqlBulkCopyColumnMapping(prop.Name, prop.Name)).ToList();
            var etlInsertTimeMapping = new SqlBulkCopyColumnMapping(EtlInsertTime, EtlInsertTime);
            mappings.Add(etlInsertTimeMapping);

            return mappings;
        }

        public DataTable GetDataTable<TMap>(CsvReader reader, DateTime etlInsertTime) where TMap : CsvClassMap<T>
        {
            reader.Configuration.RegisterClassMap<TMap>();

            var dataTable = new DataTable(nameof(T))
            {
                Locale = CultureInfo.InvariantCulture
            };
            ConfigureDataTableColumns(dataTable);

            while (reader.Read())
            {
                var record = reader.GetRecord<T>();
                var row = dataTable.NewRow();
                foreach (var prop in _properties)
                {
                    var value = prop.GetValue(record, null);
                    row[prop.Name] = value ?? (object)DBNull.Value;
                }

                row[EtlInsertTime] = etlInsertTime;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}