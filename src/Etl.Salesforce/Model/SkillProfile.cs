using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class SkillProfile: SalesforceObject
    {
        public static readonly string ObjectName = "SKILLPROFILE";
        public static readonly string Query = @"SELECT CreatedById,CreatedDate,Id,IsDeleted,LastModifiedById,LastModifiedDate,ProfileId,SkillId,SystemModstamp FROM SkillProfile";

        public string Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ProfileId { get; set; }
        public string SkillId { get; set; }
        public DateTime SystemModstamp { get; set; }
    }
}
