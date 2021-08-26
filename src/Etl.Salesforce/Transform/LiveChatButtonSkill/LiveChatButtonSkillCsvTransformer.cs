using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveChatButtonSkill
{
    public sealed class LiveChatButtonSkillCsvTransformer : CsvToDataTableTransformer<Model.LiveChatButtonSkill>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveChatButtonSkillMap>);
        }
    }
}
