using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
	public class ClassroomTraining : SalesforceObject
	{
		public static readonly string ObjectName = "CLASSROOMTRAINING__C";
		public static readonly string Query = "SELECT Country__c, CreatedById, CreatedDate, CurrencyIsoCode, Description__c, EmailTo__c, Email_notes__c, General_Info__c, " +
											  "Group__c, Id, IsDeleted, Language__c, LastModifiedById, LastModifiedDate, LastReferencedDate, LastViewedDate, Name, OwnerId, " +
											  "Short_Description__c, ShowSessions__c, SystemModstamp, Title__c, Topic__c FROM ClassroomTraining__c";

		public string Country__c { get; set; }
		public string CreatedById { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CurrencyIsoCode { get; set; }
		public string Description__c { get; set; }
		public string EmailTo__c { get; set; }
		public string Email_notes__c { get; set; }
		public string General_Info__c { get; set; }
		public string Group__c { get; set; }
		public string Id { get; set; }
		public bool? IsDeleted { get; set; }
		public string Language__c { get; set; }
		public string LastModifiedById { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public DateTime? LastReferencedDate { get; set; }
		public DateTime? LastViewedDate { get; set; }
		public string Name { get; set; }
		public string OwnerId { get; set; }
		public string Short_Description__c { get; set; }
		public bool? ShowSessions__c { get; set; }
		public DateTime? SystemModstamp { get; set; }
		public string Title__c { get; set; }
		public string Topic__c { get; set; }
	}
}
