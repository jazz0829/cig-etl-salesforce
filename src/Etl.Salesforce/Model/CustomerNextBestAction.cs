using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class CustomerNextBestAction : SalesforceObject
    {
        public static readonly string ObjectName = "CUSTOMER_NEXT_BEST_ACTION__C";
        public static readonly string Query = @"SELECT Account__c,Action_Title__c,Action__c,Case__c,CIG_ID__c,Contact__c,CreatedDate,Id,IsDeleted,LastModifiedDate, " +
            "IsTaken__c,Is_Suggested__c,Name,Notes__c,Success_Impact_estimate__c, Action_Taken_by_User__c, Action_Taken_Date__c, LastModifiedById, SystemModstamp, OwnerId FROM Customer_Next_Best_Action__c";

        public string Id { get; set; }
        public string Account__c { get; set; }
        public string Action_Title__c { get; set; }
        public string Action__c { get; set; }
        public string Case__c { get; set; }
        public string CIG_ID__c { get; set; }
        public string Contact__c { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsTaken__c { get; set; }
        public bool Is_Suggested__c { get; set; }
        public string Name { get; set; }
        public string Success_Impact_estimate__c { get; set; }
        public string Notes__c { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Action_Taken_by_User__c { get; set; }
        public DateTime? Action_Taken_Date__c { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime? SystemModstamp { get; set; }
        public string OwnerId { get; set; }
    }
}
