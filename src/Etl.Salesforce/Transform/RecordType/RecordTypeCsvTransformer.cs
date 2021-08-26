using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.RecordType
{
    public class RecordTypeCsvTransformer: CsvToDataTableTransformer<Model.RecordType>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<RecordTypeMap>);
        }
    }
}
