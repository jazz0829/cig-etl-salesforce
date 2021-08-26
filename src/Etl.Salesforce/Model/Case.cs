using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class Case : SalesforceObject
    {
        public static readonly string ObjectName = "CASE";
        public static readonly string Query = @"SELECT Id, Latest_chat_transcript__c , AccountId, ContactId, CaseNumber, ClosedDate, CreatedDate, Description, IsClosed, IsDeleted, IsEscalated, LastModifiedDate, " +
                                              @"LastReferencedDate, LastViewedDate, Origin, Priority, Reason, RecordTypeId, Status, Subject, Type, CreatedById, LastModifiedById, OwnerId, " +
                                              @"Account_Number__c, Call_me_back__c, Case_subject__c, Date_Time_Answered__c, Enddate__c, Exact_Creation_Date__c, Exact_ID__c, Exact_Request_Number__c,"
                                              + @" Main_Reason__c, Planned_End_Date__c, Reject_Reason__c, Solution__c, Startdate__c, Start_Date__c, Sub_Reason__c, Owner_Role__c, Related_case__c, "
                                              + @"Account_Hosting_Environment__c, X2nd_Line_Help__c, Date_Time_Reopened__c, Number_of_times_reopened__c, Reason_Complaint__c, Sub_Reason_Complaint__c, "+
                                              @"Last_Public_Case_Comment__c, Last_Public_Comment_Date__c, Related_Account__c, Related_Contact__c, Action_follow_up_date__c, Churn_Reason__c, Consultancy__c,  " +
                                              @"ContactEmail, ContactFax, ContactMobile, ContactName__c, ContactPhone, Contact_FirstName__c, Customer_type__c, End_Stage_Check_up__c, End_Stage_Closing__c,  " +
                                              @"End_Stage_New__c, End_Stage_Welcome__c, Number_of_outbound__c, Planned_go_live__c, Reason_for_no_consult__c, Stage_Closed__c, Stage_Phase__c FROM Case";

        public string Id { get; set; }
        public string AccountId { get; set; }
        public string ContactId { get; set; }
        public string CaseNumber { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEscalated { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? LastReferencedDate { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public string Origin { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
        public string RecordTypeId { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string CreatedById { get; set; }
        public string LastModifiedById { get; set; }
        public string OwnerId { get; set; }
        public string Account_Number__c { get; set; }
        public bool? Call_me_back__c { get; set; }
        public string Case_subject__c { get; set; }
        public DateTime? Date_Time_Answered__c { get; set; }
        public DateTime? Enddate__c { get; set; }
        public DateTime? Exact_Creation_Date__c { get; set; }
        public string Exact_ID__c { get; set; }
        public string Exact_Request_Number__c { get; set; }
        public string Main_Reason__c { get; set; }
        public DateTime? Planned_End_Date__c { get; set; }
        public string Reject_Reason__c { get; set; }
        public string Solution__c { get; set; }
        public DateTime? Startdate__c { get; set; }
        public DateTime? Start_Date__c { get; set; }
        public string Sub_Reason__c { get; set; }
        public string Owner_Role__c { get; set; }
        public string Related_case__c { get; set; }
        public string Account_Hosting_Environment__c { get; set; }
        public bool X2nd_Line_Help__c { get; set; }
        public DateTime? Date_Time_Reopened__c { get; set; }
        public float? Number_of_times_reopened__c { get; set; }
        public string Reason_Complaint__c { get; set; }
        public string Sub_Reason_Complaint__c { get; set; }

        public string Latest_chat_transcript__c { get; set; }
        public DateTime? Last_Public_Comment_Date__c { get; set; }
        public string Last_Public_Case_Comment__c { get; set; }
        public string Related_Account__c { get; set; }
        public string Related_Contact__c { get; set; }

        public DateTime? Action_follow_up_date__c { get; set; }
        public string Churn_Reason__c { get; set; }
        public string Consultancy__c { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public string ContactMobile { get; set; }
        public string ContactName__c { get; set; }
        public string ContactPhone { get; set; }
        public string Contact_FirstName__c { get; set; }
        public string Customer_type__c { get; set; }
        public DateTime? End_Stage_Check_up__c { get; set; }
        public DateTime? End_Stage_Closing__c { get; set; }
        public DateTime? End_Stage_New__c { get; set; }
        public DateTime? End_Stage_Welcome__c { get; set; }
        public decimal? Number_of_outbound__c { get; set; }
        public DateTime? Planned_go_live__c { get; set; }
        public string Reason_for_no_consult__c { get; set; }
        public DateTime? Stage_Closed__c { get; set; }
        public string Stage_Phase__c { get; set; }
    }
}
