using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class IndividualEmailResult: SalesforceObject
    {
        public static readonly string ObjectName = "ET4AE5__INDIVIDUALEMAILRESULT__C";

        public static readonly string Query =
            "SELECT CreatedById,CreatedDate,et4ae5__CampaignMemberId__c,et4ae5__Clicked__c,et4ae5__Contact_ID__c,et4ae5__Contact__c,et4ae5__DateBounced__c,et4ae5__DateOpened__c,et4ae5__DateSent__c,et4ae5__DateUnsubscribed__c,et4ae5__Email_Asset_ID__c,et4ae5__Email_ID__c,et4ae5__Email__c,et4ae5__FromAddress__c,et4ae5__FromName__c,et4ae5__HardBounce__c,et4ae5__Lead_ID__c,et4ae5__Lead__c,et4ae5__MergeId__c,et4ae5__NumberOfTotalClicks__c,et4ae5__NumberOfUniqueClicks__c,et4ae5__Opened__c,et4ae5__SendDefinition__c,et4ae5__SoftBounce__c,et4ae5__SubjectLine__c,et4ae5__Tracking_As_Of__c,et4ae5__TriggeredSendDefinitionName__c,et4ae5__TriggeredSendDefinition__c,Id,IsDeleted,LastModifiedById,LastModifiedDate,Name,OwnerId FROM et4ae5__IndividualEmailResult__c";


        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string et4ae5__CampaignMemberId__c { get; set; }
        public bool? et4ae5__Clicked__c { get; set; }
        public string et4ae5__Contact_ID__c { get; set; }
        public string et4ae5__Contact__c { get; set; }
        public DateTime? et4ae5__DateBounced__c { get; set; }
        public DateTime? et4ae5__DateOpened__c { get; set; }
        public DateTime? et4ae5__DateSent__c { get; set; }
        public DateTime? et4ae5__DateUnsubscribed__c { get; set; }
        public string et4ae5__Email_Asset_ID__c { get; set; }
        public string et4ae5__Email_ID__c { get; set; }
        public string et4ae5__Email__c { get; set; }
        public string et4ae5__FromAddress__c { get; set; }
        public string et4ae5__FromName__c { get; set; }
        public bool? et4ae5__HardBounce__c { get; set; }
        public string et4ae5__Lead_ID__c { get; set; }
        public string et4ae5__Lead__c { get; set; }
        public string et4ae5__MergeId__c { get; set; }
        public decimal? et4ae5__NumberOfTotalClicks__c { get; set; }
        public decimal? et4ae5__NumberOfUniqueClicks__c { get; set; }
        //public string et4ae5__NumberOfTotalClicks__c { get; set; }
        //public string et4ae5__NumberOfUniqueClicks__c { get; set; }
        public bool? et4ae5__Opened__c { get; set; }
        public string et4ae5__SendDefinition__c { get; set; }
        public bool? et4ae5__SoftBounce__c { get; set; }
        public string et4ae5__SubjectLine__c { get; set; }
        public DateTime? et4ae5__Tracking_As_Of__c { get; set; }
        public string et4ae5__TriggeredSendDefinitionName__c { get; set; }
        public string et4ae5__TriggeredSendDefinition__c { get; set; }
    }
}
