using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.SkillUser
{
    public sealed class SkillUserCsvTransformer : CsvToDataTableTransformer<Model.SkillUser>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<SkillUserMap>);
        }
    }
}
