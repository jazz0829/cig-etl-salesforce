using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.SkillProfile
{
    public sealed class SkillProfileCsvTransformer : CsvToDataTableTransformer<Model.SkillProfile>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<SkillProfileMap>);
        }
    }
}
