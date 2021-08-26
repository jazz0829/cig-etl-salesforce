using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class User: SalesforceObject
    {
        public static readonly string ObjectName = "USER";
        public static readonly string Query = @"SELECT Id, UserID__c, CommunityNickname, UserType, ProfileId, ContactId, ExactIDAccount__c, LastModifiedDate, Alias, FirstName, MiddleName" 
                                              + @", LastName, Name, FederationIdentifier FROM User";

        public string Id { get; set; }
        public string UserID__c { get; set; }
        public string CommunityNickname { get; set; }
        public string UserType { get; set; }
        public string ProfileId { get; set; }
        public string ContactId { get; set; }
        public string ExactIDAccount__c { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FederationIdentifier { get; set; }
    }
}
