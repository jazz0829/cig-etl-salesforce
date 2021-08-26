using System.Globalization;
using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Transform.CaseComment
{
    public class CaseCommentMap: CsvClassMap<Model.CaseComment>
    {
        public CaseCommentMap()
        {
            AutoMap();
            Map(m => m.LastModifiedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            Map(m => m.CreatedDate).TypeConverterOption(DateTimeStyles.AdjustToUniversal);
        }
    }
}
