using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveChatButtonSkill: SalesforceObject
    {
        public static readonly string ObjectName = "LIVECHATBUTTONSKILL";
        public static readonly string Query = @"SELECT ButtonId,CreatedById,CreatedDate,Id,IsDeleted,LastModifiedById,LastModifiedDate,SkillId,SystemModstamp FROM LiveChatButtonSkill";

        public string Id { get; set; }
        public string ButtonId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string SkillId { get; set; }
        public DateTime SystemModstamp { get; set; }
    }
}
