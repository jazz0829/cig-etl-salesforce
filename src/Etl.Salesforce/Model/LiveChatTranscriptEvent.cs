using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveChatTranscriptEvent: SalesforceObject
    {
        public static readonly string ObjectName = "LIVECHATTRANSCRIPTEVENT";

        public static readonly string Query =
            @"SELECT AgentId,CreatedById,CreatedDate,Detail,Id,IsDeleted,LastModifiedById,LastModifiedDate,LiveChatTranscriptId,Name,SystemModstamp,Time,Type FROM LiveChatTranscriptEvent";

        public string Id { get; set; }
        public string AgentId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public  DateTime LastModifiedDate { get; set; }
        public string Detail { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public string LiveChatTranscriptId { get; set; }
        public string Name { get; set; }
        public DateTime SystemModstamp { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
    }
}
