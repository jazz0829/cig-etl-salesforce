using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class KeyEvent: SalesforceObject
    {
        public static readonly string ObjectName = "KEYEVENT__C";
        public static readonly string Query = @"SELECT Id,ContactID__c, CreatedDate FROM KeyEvent__c";

        public string Id { get; set; }
        public string ContactID__c { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
