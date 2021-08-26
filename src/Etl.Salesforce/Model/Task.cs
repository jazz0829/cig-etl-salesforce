using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
	public class Task : SalesforceObject
	{
		public static readonly string ObjectName = "TASK";
		public static readonly string Query = @"SELECT Accountancy_Profile__c,Accountancy_Programm__c,AccountId,Account_Information__c,Account_Manager__c,Action_follow_up_date__c, " +
											  @"ActivityDate,Article_Language__c,Article_URL_Link__c,Article_URL_Name__c,CallDisposition,CallDurationInSeconds,CallObject, " +
											  @"CallType,Cancellation_reason__c,Cancellation__c,Churn_notes__c,Churn_Reason_P_P__c,Churn_Reason__c,Churn_risk__c, " +
											  @"Completion_Date__c,Consultancy_Request__c,CreatedById,CreatedDate,CurrencyIsoCode,Customer_Username__c,Description,Details_of_Status__c, " +
											  @"Expected_cancellation_amount__c,Expected_term_of_cancellation__c,Follow_up_case__c,Help_Tools__c,Hosting_Request_other__c,Id, " +
											  @"IsArchived,IsClosed,IsDeleted,IsHighPriority,IsRecurrence,IsReminderSet,LastModifiedById,LastModifiedDate,Link__c,Login_Data__c, " +
											  @"My_Exact_Online__c,Other_Project__c,Outcome_Meeting__c,OwnerId,Priority,Project__c,Reached__c,RecordTypeId,RecurrenceActivityId, " +
											  @"RecurrenceDayOfMonth,RecurrenceDayOfWeekMask,RecurrenceEndDateOnly,RecurrenceInstance,RecurrenceInterval,RecurrenceMonthOfYear, " +
											  @"RecurrenceRegeneratedType,RecurrenceStartDateOnly,RecurrenceTimeZoneSidKey,RecurrenceType,Related_Account__c,Related_Contact__c, " +
											  @"Related_Project__c,ReminderDateTime,Sales_Opportunity__c,Satisfaction_notes__c,Satisfaction__c,Status,Status_notes__c,Subject, " +
											  @"SystemModstamp,TaskSubtype,Tips__c,Type,Welcome__c,WhatId,WhoId FROM Task";

		public string Accountancy_Profile__c { get; set; }
		public string Accountancy_Programm__c { get; set; }
		public string AccountId { get; set; }
		public string Account_Information__c { get; set; }
		public string Account_Manager__c { get; set; }
		public DateTime? Action_follow_up_date__c { get; set; }
		public DateTime? ActivityDate { get; set; }
		public string Article_Language__c { get; set; }
		public string Article_URL_Link__c { get; set; }
		public string Article_URL_Name__c { get; set; }
		public string CallDisposition { get; set; }
		public int? CallDurationInSeconds { get; set; }
		public string CallObject { get; set; }
		public string CallType { get; set; }
		public string Cancellation_reason__c { get; set; }
		public string Cancellation__c { get; set; }
		public string Churn_notes__c { get; set; }
		public string Churn_Reason_P_P__c { get; set; }
		public string Churn_Reason__c { get; set; }
		public string Churn_risk__c { get; set; }
		public DateTime? Completion_Date__c { get; set; }
		public string Consultancy_Request__c { get; set; }
		public string CreatedById { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CurrencyIsoCode { get; set; }
		public string Customer_Username__c { get; set; }
		public string Description { get; set; }
		public string Details_of_Status__c { get; set; }
		public decimal? Expected_cancellation_amount__c { get; set; }
		public string Expected_term_of_cancellation__c { get; set; }
		public string Follow_up_case__c { get; set; }
		public string Help_Tools__c { get; set; }
		public string Hosting_Request_other__c { get; set; }
		public string Id { get; set; }
		public bool? IsArchived { get; set; }
		public bool? IsClosed { get; set; }
		public bool? IsDeleted { get; set; }
		public bool? IsHighPriority { get; set; }
		public bool? IsRecurrence { get; set; }
		public bool? IsReminderSet { get; set; }
		public string LastModifiedById { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public string Link__c { get; set; }
		public string Login_Data__c { get; set; }
		public string My_Exact_Online__c { get; set; }
		public string Other_Project__c { get; set; }
		public string Outcome_Meeting__c { get; set; }
		public string OwnerId { get; set; }
		public string Priority { get; set; }
		public string Project__c { get; set; }
		public string Reached__c { get; set; }
		public string RecordTypeId { get; set; }
		public string RecurrenceActivityId { get; set; }
		public string RecurrenceDayOfMonth { get; set; }
		public int? RecurrenceDayOfWeekMask { get; set; }
		public DateTime? RecurrenceEndDateOnly { get; set; }
		public string RecurrenceInstance { get; set; }
		public int? RecurrenceInterval { get; set; }
		public string RecurrenceMonthOfYear { get; set; }
		public string RecurrenceRegeneratedType { get; set; }
		public DateTime? RecurrenceStartDateOnly { get; set; }
		public string RecurrenceTimeZoneSidKey { get; set; }
		public string RecurrenceType { get; set; }
		public string Related_Account__c { get; set; }
		public string Related_Contact__c { get; set; }
		public string Related_Project__c { get; set; }
		public DateTime? ReminderDateTime { get; set; }
		public string Sales_Opportunity__c { get; set; }
		public string Satisfaction_notes__c { get; set; }
		public string Satisfaction__c { get; set; }
		public string Status { get; set; }
		public string Status_notes__c { get; set; }
		public string Subject { get; set; }
		public DateTime? SystemModstamp { get; set; }
		public string TaskSubtype { get; set; }
		public string Tips__c { get; set; }
		public string Type { get; set; }
		public string Welcome__c { get; set; }
		public string WhatId { get; set; }
		public string WhoId { get; set; }


	}
}
