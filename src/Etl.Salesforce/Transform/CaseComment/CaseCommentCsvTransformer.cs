using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.CaseComment
{
    public sealed class CaseCommentCsvTransformer: CsvToDataTableTransformer<Model.CaseComment>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<CaseCommentMap>);
        }
    }
}
