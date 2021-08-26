using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.KeyEvent
{
    public sealed class KeyEventCsvTransformer : CsvToDataTableTransformer<Model.KeyEvent>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<KeyEventMap>);
        }
    }
}
