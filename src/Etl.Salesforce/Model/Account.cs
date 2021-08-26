using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class Account: SalesforceObject
    {
        public static readonly string ObjectName = "ACCOUNT";
        public static readonly string Query = @"SELECT Id, Exact_ID__c, AccountNumber, LastModifiedDate, Active__c, Business_Type__c, Company_Size__c, Contract_value__c, ControlledRelease__c," +
                                              @" Enddate__c, Industry, IsPartner, Name, Package__c, Sector__c, Status__c, Subscription__c, Type FROM Account";

        public string Id { get; set; }
        public string Exact_ID__c { get; set; }
        public string AccountNumber { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Active__c { get; set; }
        public string Business_Type__c { get; set; }
        public string Company_Size__c { get; set; }
        public float? Contract_value__c { get; set; }
        public bool ControlledRelease__c { get; set; }
        public DateTime? Enddate__c { get; set; }
        public string Industry { get; set; }
        public bool IsPartner { get; set; }
        public string Name { get; set; }
        public string Package__c { get; set; }
        public string Sector__c { get; set; }
        public string Status__c { get; set; }
        public string Subscription__c { get; set; }
        public string Type { get; set; }
    }
}
