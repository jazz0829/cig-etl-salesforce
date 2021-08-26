using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class RecordType: SalesforceObject
    {
        public static readonly string ObjectName = "RECORDTYPE";
        public static readonly string Query = @"SELECT Id, Name, Description, IsActive, BusinessProcessId, CreatedById, CreatedDate, DeveloperName, LastModifiedById, LastModifiedDate," + 
                                              @" NamespacePrefix, SobjectType, SystemModstamp FROM RecordType";

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string BusinessProcessId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DeveloperName { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string NamespacePrefix { get; set; }
        public string SobjectType { get; set; }
        public DateTime SystemModstamp { get; set; }
    }
}