using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveChatVisitor: SalesforceObject
    {
        public static readonly string ObjectName = "LIVECHATVISITOR";

        public static readonly string Query =
            @"SELECT CreatedById,CreatedDate,Id,IsDeleted,LastModifiedById,LastModifiedDate,Name,SessionKey,SystemModstamp FROM LiveChatVisitor";

        public string Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Name { get; set; }
        public string SessionKey { get; set; }
        public DateTime SystemModstamp { get; set; }
    }
}
