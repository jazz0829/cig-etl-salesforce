using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Contact
{
    public sealed class ContactCsvTransformer: CsvToDataTableTransformer<Model.Contact>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<ContactMap>);
        }
    }
}