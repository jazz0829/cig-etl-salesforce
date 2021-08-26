using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class LiveAgentSession: SalesforceObject
    {
        public static readonly string ObjectName = "LIVEAGENTSESSION";
        public static readonly string Query = @"SELECT AgentId,ChatReqAssigned,ChatReqDeclined,ChatReqEngaged,ChatReqTimedOut,CreatedById,CreatedDate,Id,IsDeleted,LastModifiedById,LastModifiedDate,LoginTime,LogoutTime,Name,NumFlagLoweredAgent,NumFlagLoweredSupervisor,NumFlagRaised,OwnerId,SystemModstamp,TimeAtCapacity,TimeIdle,TimeInAwayStatus,TimeInChats,TimeInOnlineStatus FROM LiveAgentSession";

        public string Id { get; set; }
        public string AgentId { get; set; }
        public  int ChatReqAssigned { get; set; }
        public int ChatReqDeclined { get; set; }
        public int ChatReqEngaged { get; set; }
        public int ChatReqTimedOut { get;set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public string Name { get; set; }
        public int NumFlagLoweredAgent { get; set; }
        public int NumFlagLoweredSupervisor { get; set; }
        public int NumFlagRaised { get; set; }
        public string OwnerId { get; set; }
        public DateTime SystemModstamp { get; set; }
        public int TimeAtCapacity { get; set; }
        public int TimeIdle { get; set; }
        public int TimeInAwayStatus { get; set; }
        public int TimeInChats { get; set; }
        public int TimeInOnlineStatus { get; set; }
    }
}
