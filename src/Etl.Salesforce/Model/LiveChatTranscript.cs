using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveChatTranscript : SalesforceObject
    {
        public static readonly string ObjectName = "LIVECHATTRANSCRIPT";
        public static readonly string Query = @"SELECT Body,Abandoned,AccountId,AverageResponseTimeOperator,AverageResponseTimeVisitor,Browser,BrowserLanguage,CaseId,ChatDuration,ChatKey,ContactId,CreatedById,CreatedDate,EndedBy,EndTime,Id,IpAddress,IsDeleted,LastModifiedById,LastModifiedDate,LeadId,LiveChatButtonId,LiveChatDeploymentId,LiveChatVisitorId,Location,MaxResponseTimeOperator,MaxResponseTimeVisitor,Name,OperatorMessageCount,OwnerId,Platform,ReferrerUri,RequestTime,ScreenResolution,SkillId,StartTime,Status,SupervisorTranscriptBody,SystemModstamp,UserAgent,VisitorMessageCount,WaitTime FROM LiveChatTranscript";

        public string Id { get; set; }
        public int? Abandoned { get; set; }
        public string AccountId { get; set; }
        public int? AverageResponseTimeOperator { get; set; }
        public int? AverageResponseTimeVisitor { get; set; }
        public string Body { get; set; }
        public string Browser { get; set; }
        public string BrowserLanguage { get; set; }
        public string CaseId { get; set; }
        public int? ChatDuration { get; set; }
        public string ChatKey { get; set; }
        public string ContactId { get; set; }
        public string CreatedById { get; set; }
        public string EndedBy { get; set; }
        public string IpAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime SystemModstamp { get; set; }
        public DateTime? RequestTime { get; set; }
        public string LeadId { get; set; }
        public string LiveChatButtonId { get; set; }
        public string LiveChatDeploymentId { get; set; }
        public string LiveChatVisitorId { get; set; }
        public string Location { get; set; }
        public int? MaxResponseTimeOperator { get; set; }
        public int? MaxResponseTimeVisitor { get; set; }
        public string Name { get; set; }
        public int? OperatorMessageCount { get; set; }
        public string OwnerId { get; set; }
        public string Platform { get; set; }
        public string ReferrerUri { get; set; }
        public string ScreenResolution { get; set; }
        public string SkillId { get; set; }
        public string Status { get; set; }
        public string SupervisorTranscriptBody { get; set; }
        public string UserAgent { get; set; }
        public int? VisitorMessageCount { get; set; }
        public int? WaitTime { get; set; }
    }
}
