using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveChatButton: SalesforceObject
    {
        public static readonly string ObjectName = "LIVECHATBUTTON";

        public static readonly string Query =
            @"SELECT AutoGreeting,ChasitorIdleTimeout,ChasitorIdleTimeoutWarning,ChatPageId,CreatedById,CreatedDate,CustomAgentName,DeveloperName,HasQueue,Id,IsActive,IsDeleted,Language,LastModifiedById,LastModifiedDate,MasterLabel,OptionsHasChasitorIdleTimeout,OptionsHasInviteAfterAccept,OptionsHasInviteAfterReject,OptionsHasRerouteDeclinedRequest,OptionsIsAutoAccept,OptionsIsInviteAutoRemove,OverallQueueLength,PerAgentQueueLength,RoutingType,SkillId,SystemModstamp,Type,WindowLanguage FROM LiveChatButton";

        public string Id { get; set; }
        public string AutoGreeting { get; set; }
        public int? ChasitorIdleTimeout { get; set; }
        public int? ChasitorIdleTimeoutWarning { get; set; }
        public string ChatPageId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomAgentName { get; set; }
        public string DeveloperName { get; set; }
        public bool? HasQueue { get; set; }
        public bool? IsActive { get; set; }
        //public bool IsAgentOnlineNeeded { get; set; } we don't have access on it right now
        public bool? IsDeleted { get; set; }
        public string Language { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string MasterLabel { get; set; }
        public bool? OptionsHasChasitorIdleTimeout { get; set; }
        public bool? OptionsHasInviteAfterAccept { get; set; }
        public bool? OptionsHasInviteAfterReject { get; set; }
        public bool? OptionsHasRerouteDeclinedRequest { get; set; }
        public bool? OptionsIsAutoAccept { get; set; }
        public bool? OptionsIsInviteAutoRemove { get; set; }
        public int? OverallQueueLength { get; set; }
        public int? PerAgentQueueLength { get; set; }
        public string RoutingType { get; set; }
        public string SkillId { get; set; }
        public DateTime SystemModstamp { get; set; }
        public string Type { get; set; }
        public string WindowLanguage { get; set; }
    }
}
