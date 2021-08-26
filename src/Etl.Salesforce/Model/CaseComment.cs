using System;

namespace Eol.Cig.Etl.Salesforce.Model
{

    public class CaseComment : SalesforceObject
    {
        public static readonly string ObjectName = "CASECOMMENT";
        public static readonly string Query = @"SELECT Id, CommentBody, CreatedById, CreatedDate, IsDeleted, IsPublished, LastModifiedById, LastModifiedDate, ParentId FROM CaseComment";

        public string Id { get; set; }
        public string CommentBody { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ParentId { get; set; }
    }
}
