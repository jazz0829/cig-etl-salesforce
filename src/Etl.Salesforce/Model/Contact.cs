using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class Contact: SalesforceObject
    {
        public static readonly string ObjectName = "CONTACT";
        public static readonly string Query = @"SELECT Id, FirstName, LastName, AccountId, CreatedDate, IsDeleted, IsEmailBounced, LastActivityDate, LastCURequestDate, LastCUUpdateDate," +
                                              @" LastModifiedDate, LastViewedDate, Enddate__c, UserID__c, EOL_User__c, Exact_ID__c, Gender__c, Language__c, Startdate__c FROM Contact";

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedDate  { get; set; }
        public bool?  IsDeleted { get; set; }
        public bool? IsEmailBounced { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastCURequestDate { get; set; }
        public DateTime? LastCUUpdateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public string UserID__c { get; set; }
        public bool? EOL_User__c { get; set; }
        public string Exact_ID__c { get; set; }
        public string Gender__c { get; set; }
        public string Language__c { get; set; }
        public DateTime? Startdate__c { get; set; }
        public DateTime? Enddate__c { get; set; }
    }
}
