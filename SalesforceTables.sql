CREATE TABLE [raw].[Salesforce_Account]
(
	Id VARCHAR(20) NULL,
	Exact_ID__c VARCHAR(50) NULL,
	AccountNumber VARCHAR(20) NULL,
	LastModifiedDate DATETIME,
	EtlInsertTime DATETIME NULL
);

GO

CREATE TABLE [raw].[Salesforce_CaseComment]
(
	Id VARCHAR(20) NULL,
	CommentBody NVARCHAR(MAX) NULL,
	CreatedById VARCHAR(20) NULL,
	CreatedDate DATETIME NULL,
	IsDeleted BIT NULL,
	IsPublished BIT NULL,
	LastModifiedById VARCHAR(20) NULL,
	LastModifiedDate DATETIME NULL,
	ParentId VARCHAR(20) NULL,
	EtlInsertTime DATETIME NOT NULL
);

GO

CREATE TABLE [raw].[Salesforce_Contact]
(
	Id VARCHAR(20) NULL,
	FirstName NVARCHAR(100) NULL,
	LastName NVARCHAR(100) NULL,
	AccountId VARCHAR(20) NULL,
	CreatedDate DATETIME NULL,
	IsDeleted BIT NULL,
	IsEmailBounced BIT NULL,
	LastActivityDate DATETIME,
	LastCURequestDate DATETIME,
	LastCUUpdateDate DATETIME,
	LastModifiedDate DATETIME,
	LastViewedDate DATETIME,
	Enddate__c DATETIME NULL,
	UserID__c VARCHAR(50),
	EOL_User__c BIT NULL ,
	Exact_ID__c VARCHAR(50),
	Gender__c VARCHAR(10),
	Language__c VARCHAR(50),
	Startdate__c DATETIME,
	EtlInsertTime DATETIME NOT NULL
)

GO

CREATE TABLE [raw].[Salesforce_Case]
(
	Id VARCHAR(20) NULL,
	AccountId VARCHAR(20) NULL,
	ContactId VARCHAR(50) NULL,
	CaseNumber VARCHAR(20) NULL,
	ClosedDate DATETIME NULL,
	CreatedDate DATETIME NULL,
	[Description] NVARCHAR(MAX) NULL,
	IsClosed BIT NULL,
	IsDeleted BIT NULL,
	IsEscalated BIT NULL,
	LastModifiedDate DATETIME NULL,
	LastReferencedDate DATETIME NULL,
	LastViewedDate DATETIME NULL,
	Origin VARCHAR(100) NULL,
	[Priority] VARCHAR(50) NULL,
	Reason VARCHAR(100) NULL,
	RecordTypeId VARCHAR(20) NULL,
	[Status] VARCHAR(50) NULL,
	[Subject] NVARCHAR(MAX) NULL,
	[Type] VARCHAR(100) NULL,
	CreatedById VARCHAR(20) NULL,
	LastModifiedById VARCHAR(20) NULL,
	OwnerId VARCHAR(20) NULL,
	Account_Number__c VARCHAR(20) NULL,
	Call_me_back__c BIT NULL,
	Case_subject__c VARCHAR(50) NULL,
	Date_Time_Answered__c DATETIME NULL,
	Enddate__c DATETIME NULL,
	Exact_Creation_Date__c DATETIME NULL,
	Exact_ID__c VARCHAR(50) NULL,
	Exact_Request_Number__c VARCHAR(50) NULL,
	Main_Reason__c VARCHAR(100) NULL,
	Planned_End_Date__c DATETIME NULL,
	Reject_Reason__c NVARCHAR(MAX) NULL,
	Solution__c NVARCHAR(MAX) NULL,
	Startdate__c DATETIME NULL,
	Start_Date__c DATETIME NULL,
	Sub_Reason__c VARCHAR(256) NULL,
	Owner_Role__c VARCHAR(256) NULL,
	Related_case__c VARCHAR(20) NULL,
	Account_Hosting_Environment__c VARCHAR(5) NULL,
	EtlInsertTime DATETIME NOT NULL
);

CREATE TABLE [raw].[Salesforce_RecordType] (
	Id VARCHAR(20) NULL,
	[Name] VARCHAR(100) NULL,
	[Description] VARCHAR(500) NULL,
	IsActive BIT NULL,
	BusinessProcessId VARCHAR(20) NULL,
	CreatedById VARCHAR(20) NULL,
	CreatedDate DATETIME NULL,
	DeveloperName VARCHAR(100) NULL,
	LastModifiedById VARCHAR(20) NULL,
	LastModifiedDate DATETIME NULL,
	NamespacePrefix VARCHAR(100) NULL,
	SobjectType VARCHAR(100) NULL,
	SystemModstamp DATETIME NULL,
	EtlInsertTime DATETIME NULL
);

GO

CREATE TABLE [raw].[Salesforce_User] (
	Id VARCHAR(20) NULL,
	UserID__c VARCHAR(50) NULL,
	CommunityNickname VARCHAR(200) NULL,
	UserType VARCHAR(20) NULL,
	ProfileId VARCHAR(50) NULL,
	ContactId VARCHAR(50) NULL,
	ExactIDAccount__c VARCHAR(50) NULL,
	LastModifiedDate DATETIME NULL,
	EtlInsertTime DATETIME NOT NULL
);

GO

CREATE TABLE [config].[Salesforce_DataExportLog](
	ObjectName VARCHAR(100) NOT NULL,
	LastModifiedDate DATETIME NOT NULL,
	InsertTime DATETIME NOT NULL
);

GO

CREATE TABLE [raw].[Salesforce_Survey]
(
	Id VARCHAR(20) NOT NULL,
	Answer_1__c INT NULL,
	Answer_2__c INT NULL,
	Answer_3__c INT NULL,
	Answer_4__c INT NULL,
	Answer_5__c INT NULL,
	Answer_6__c INT NULL,
	Answer_7__c INT NULL,
	Answer_8__c VARCHAR(1000) NULL,
	Average_A1_A4__c FLOAT NOT NULL,
	Average_A4_7__c FLOAT NOT NULL,
	Case__c VARCHAR(20) NULL,
	Converted_Answer_1__c FLOAT NOT NULL,
	Converted_Answer_2__c FLOAT NOT NULL,
	Converted_Answer_3__c FLOAT NOT NULL,
	Converted_Answer_4__c FLOAT NOT NULL,
	Converted_Answer_5__c FLOAT NOT NULL,
	Converted_Answer_6__c FLOAT NOT NULL,
	Converted_Answer_7__c FLOAT NOT NULL,
	CreatedById VARCHAR(20) NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CurrencyIsoCode VARCHAR(10) NULL,
	Detractor__c BIT NOT NULL,
	IsDeleted BIT NOT NULL,
	LastActivityDate DATETIME NULL,
	LastModifiedDate DATETIME NOT NULL,
	LastModifiedById VARCHAR(20) NOT NULL,
	[Name] VARCHAR(200) NULL,
	New_Answer_1__c INT NULL,
	New_Answer_2__c VARCHAR(100) NULL,
	New_Answer_3__c VARCHAR(5) NULL,
	New_Answer_4__c INT NULL,
	New_Answer_5__c VARCHAR(1000) NULL,
	New_Converted_Answer_1__c FLOAT NOT NULL,
	New_Converted_Answer_4__c FLOAT NOT NULL,
	New_Question_1__c VARCHAR(1000) NULL,
	New_Question_2__c VARCHAR(1000) NULL,
	New_Question_3__c VARCHAR(1000) NULL,
	New_Question_4__c VARCHAR(1000) NULL,
	New_Question_5__c VARCHAR(1000) NULL,
	Promoter__c BIT NOT NULL,
	Question_1__c VARCHAR(1000) NULL,
	Question_2__c VARCHAR(1000) NULL,
	Question_3__c VARCHAR(1000) NULL,
	Question_4__c VARCHAR(1000) NULL,
	Question_5__c VARCHAR(1000) NULL,
	Question_6__c VARCHAR(1000) NULL,
	Question_7__c VARCHAR(1000) NULL,
	Question_8__c VARCHAR(1000) NULL,
	EtlInsertTime DATETIME NOT NULL
)

create table config.Salesforce_FullExport
(
	ObjectName VARCHAR(100) NOT NULL,
	LastModifiedDate DATETIME NULL, --Stores how far we got with the export. The row will be deleted, once export is done.
	InsertTime DATETIME NULL --Keep it for consistency sake.
)

CREATE TABLE raw.Salesforce_IndividualEmailResult(
	Id VARCHAR(20) NULL,
	CreatedById VARCHAR(50) NULL,
	OwnerId VARCHAR(20) NULL,
	Name NVARCHAR(80) NULL,
	LastModifiedDate Datetime NULL,
	LastModifiedById varchar(50) null,
	IsDeleted bit null,
	CreatedDate datetime null,
	et4ae5__CampaignMemberId__c varchar(100) null,
	et4ae5__Clicked__c bit null,
	et4ae5__Contact_ID__c varchar(20) null,
	et4ae5__Contact__c varchar(20) null,
	et4ae5__DateBounced__c datetime null,
	et4ae5__DateOpened__c datetime null,
	et4ae5__DateSent__c datetime null,
	et4ae5__DateUnsubscribed__c datetime null,
	et4ae5__Email_Asset_ID__c varchar(100) null,
	et4ae5__Email_ID__c varchar(100) null,
	et4ae5__Email__c varchar(100) null,
	et4ae5__FromAddress__c varchar(100) null,
	et4ae5__FromName__c varchar(255) null,
	et4ae5__HardBounce__c bit null,
	et4ae5__Lead_ID__c varchar(100) null,
	et4ae5__Lead__c varchar(100) null,
	et4ae5__MergeId__c varchar(50) null,
	et4ae5__NumberOfTotalClicks__c decimal(6,0) null,
	et4ae5__NumberOfUniqueClicks__c decimal(6,0) null,
	et4ae5__Opened__c bit null,
	et4ae5__SendDefinition__c varchar(100) null,
	et4ae5__SoftBounce__c bit null,
	et4ae5__SubjectLine__c nvarchar(255) null,
	et4ae5__Tracking_As_Of__c datetime null,
	et4ae5__TriggeredSendDefinitionName__c varchar(100) null,
	et4ae5__TriggeredSendDefinition__c varchar(100) null,
	EtlInsertTime DATETIME NULL
)



alter table raw.Salesforce_Account add Active__c bit null
alter table raw.Salesforce_Account add Business_Type__c varchar(500) null
alter table raw.Salesforce_Account add Company_Size__c varchar(100) null
alter table raw.Salesforce_Account add Contract_value__c float null
alter table raw.Salesforce_Account add ControlledRelease__c bit null
alter table raw.Salesforce_Account add Enddate__c datetime null
alter table raw.Salesforce_Account add Industry varchar(200) null
alter table raw.Salesforce_Account add IsPartner bit null
alter table raw.Salesforce_Account add [Name] varchar(500) null
alter table raw.Salesforce_Account add Package__c varchar(500) null
alter table raw.Salesforce_Account add Sector__c varchar(200) null
alter table raw.Salesforce_Account add Status__c varchar(50) null
alter table raw.Salesforce_Account add Subscription__c varchar(200) null
alter table raw.Salesforce_Account add Type varchar(50) null


alter table raw.Salesforce_Case add X2nd_Line_Help__c bit NULL
alter table raw.Salesforce_Case add Date_Time_Reopened__c datetime NULL
alter table raw.Salesforce_Case add Number_of_times_reopened__c float NULL

alter table raw.Salesforce_User add Alias varchar(100) null
alter table raw.Salesforce_User add FirstName varchar(100) null
alter table raw.Salesforce_User add MiddleName varchar(50) null
alter table raw.Salesforce_User add LastName varchar(100) null
alter table raw.Salesforce_User add [Name] varchar(200) null




GO

--Indexes
CREATE CLUSTERED INDEX [IX_Salesforce_Account_LastModifiedDate] ON [raw].[Salesforce_Account] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_Case_LastModifiedDate] ON [raw].[Salesforce_Case] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_CaseComment_LastModifiedDate] ON [raw].[Salesforce_CaseComment] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_Contact_LastModifiedDate] ON [raw].[Salesforce_Contact] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_RecordType_LastModifiedDate] ON [raw].[Salesforce_RecordType] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_User_LastModifiedDate] ON [raw].[Salesforce_User] ([Id] ASC, [LastModifiedDate] DESC)
CREATE CLUSTERED INDEX [IX_Salesforce_Survey_LastModifiedDate] ON [raw].[Salesforce_Survey] ([Id] ASC, [LastModifiedDate] DESC)


CREATE ROLE SalesforceImport

GO


GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_Case] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_CaseComment] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_Account] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_Contact] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_RecordType] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_User] TO SalesforceImport
GRANT ALTER, SELECT, INSERT, UPDATE ON [raw].[Salesforce_Survey] TO SalesforceImport
GRANT SELECT, INSERT, UPDATE, DELETE ON config.Salesforce_FullExport TO SalesforceImport
GRANT SELECT, INSERT ON [config].[Salesforce_DataExportLog] TO SalesforceImport

GO

CREATE TABLE [raw].[Salesforce_ClassroomTraining](
	[Id] [varchar](20) NULL,
	[Name] [varchar](200) NULL,
	[Country__c] [varchar](20) NULL,
	[CurrencyIsoCode] [varchar](3) NULL,
	[General_Info__c] [varchar](5000) NULL,
	[Title__c] [varchar](100) NULL,
	[Topic__c] [varchar](255) NULL,
	[Description__c] [varchar](5000) NULL,
	[Short_Description__c] [varchar](85) NULL,
	[EmailTo__c] [varchar](100) NULL,
	[Email_notes__c] [varchar](5000) NULL,
	[Group__c] [varchar](5000) NULL,
	[IsDeleted] [bit] NULL,
	[Language__c] [varchar](100) NULL,
	[CreatedById] [varchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[LastModifiedById] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastReferencedDate] [datetime] NULL,
	[LastViewedDate] [datetime] NULL,
	[OwnerId] [varchar](20) NULL,
	[ShowSessions__c] [bit] NULL,
	[SystemModstamp] [datetime] NULL,
	[EtlInsertTime] [datetime] NULL
)

GO

alter table raw.Salesforce_User add FederationIdentifier varchar(100) null

CREATE TABLE [raw].[Salesforce_Task](
	[Id] [varchar](20) NULL,
	[Subject] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [varchar](30) NULL,
	[Details_of_Status__c] [varchar](100) NULL,
	[Status_notes__c] [varchar](100) NULL,
	[Accountancy_Profile__c] [varchar](100) NULL,
	[Accountancy_Programm__c] [varchar](100) NULL,
	[AccountId] [varchar](20) NULL,
	[Account_Information__c] [varchar](100) NULL,
	[Account_Manager__c] [varchar](100) NULL,
	[Action_follow_up_date__c] [datetime] NULL,
	[ActivityDate] [datetime] NULL,
	[Article_Language__c] [varchar](20) NULL,
	[Article_URL_Link__c] [varchar](100) NULL,
	[Article_URL_Name__c] [varchar](100) NULL,
	[CallDisposition] [varchar](255) NULL,
	[CallDurationInSeconds] [int] NULL,
	[CallObject] [varchar](255) NULL,
	[CallType] [varchar](50) NULL,
	[Cancellation_reason__c] [varchar](255) NULL,
	[Cancellation__c] [varchar](20) NULL,
	[Churn_notes__c] [nvarchar](max) NULL,
	[Churn_Reason_P_P__c] [nvarchar](max) NULL,
	[Churn_Reason__c] [nvarchar](max) NULL,
	[Churn_risk__c] [varchar](20) NULL,
	[Completion_Date__c] [datetime] NULL,
	[Consultancy_Request__c] [varchar](100) NULL,
	[CurrencyIsoCode] [varchar](3) NULL,
	[Customer_Username__c] [varchar](100) NULL,
	[Expected_cancellation_amount__c] [float] NULL,
	[Expected_term_of_cancellation__c] [varchar](50) NULL,
	[Follow_up_case__c] [varchar](100) NULL,
	[Help_Tools__c] [varchar](100) NULL,
	[Hosting_Request_other__c] [varchar](100) NULL,
	[Priority] [varchar](20) NULL,
	[IsArchived] [bit] NULL,
	[IsClosed] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[IsHighPriority] [bit] NULL,
	[IsRecurrence] [bit] NULL,
	[IsReminderSet] [bit] NULL,
	[CreatedById] [varchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[LastModifiedById] [varchar](20) NULL,
	[LastModifiedDate] [datetime] NULL,
	[Link__c] [varchar](100) NULL,
	[Login_Data__c] [varchar](100) NULL,
	[My_Exact_Online__c] [varchar](100) NULL,
	[Outcome_Meeting__c] [varchar](100) NULL,
	[OwnerId] [varchar](20) NULL,
	[Project__c] [varchar](100) NULL,
	[Other_Project__c] [varchar](100) NULL,
	[Reached__c] [varchar](100) NULL,
	[RecordTypeId] [varchar](20) NULL,
	[RecurrenceActivityId] [varchar](20) NULL,
	[RecurrenceDayOfMonth] [varchar](20) NULL,
	[RecurrenceDayOfWeekMask] [int] NULL,
	[RecurrenceEndDateOnly] [datetime] NULL,
	[RecurrenceInstance] [varchar](100) NULL,
	[RecurrenceInterval] [int] NULL,
	[RecurrenceMonthOfYear] [varchar](20) NULL,
	[RecurrenceRegeneratedType] [varchar](100) NULL,
	[RecurrenceStartDateOnly] [datetime] NULL,
	[RecurrenceTimeZoneSidKey] [varchar](100) NULL,
	[RecurrenceType] [varchar](20) NULL,
	[Related_Account__c] [varchar](20) NULL,
	[Related_Contact__c] [varchar](20) NULL,
	[Related_Project__c] [varchar](100) NULL,
	[ReminderDateTime] [datetime] NULL,
	[Sales_Opportunity__c] [varchar](20) NULL,
	[Satisfaction_notes__c] [nvarchar](max) NULL,
	[Satisfaction__c] [varchar](100) NULL,
	[SystemModstamp] [datetime] NULL,
	[Tips__c] [nvarchar](max) NULL,
	[Type] [varchar](20) NULL,
	[TaskSubtype] [varchar](10) NULL,
	[Welcome__c] [varchar](100) NULL,
	[WhatId] [varchar](100) NULL,
	[WhoId] [varchar](100) NULL,
	[EtlInsertTime] [datetime] NULL
)

GO

CREATE TABLE [raw].[Salesforce_CaseHistory](
	Id [varchar](20) NULL,
	CaseId [varchar](20) NULL,
	Field [varchar](50) NULL,
	IsDeleted [bit] NULL,
	NewValue [varchar](20) NULL,
	OldValue [varchar](20) NULL,
	CreatedById [varchar](20) NULL,
	CreatedDate [datetime] NULL,
	EtlInsertTime [datetime] NULL
)

GO

alter table raw.Salesforce_Case add Latest_chat_transcript__c nvarchar(MAX) NULL
alter table raw.Salesforce_Case add Last_Public_Case_Comment__c nvarchar(MAX) NULL
alter table raw.Salesforce_Case add Last_Public_Comment_Date__c datetime NULL
alter table raw.Salesforce_Case add Related_Account__c varchar(20) NULL
alter table raw.Salesforce_Case add Related_Contact__c varchar(20) NULL

alter table raw.Salesforce_Case add Action_follow_up_date__c [datetime] null
alter table raw.Salesforce_Case add Churn_Reason__c [nvarchar](max) null
alter table raw.Salesforce_Case add Consultancy__c [varchar](255) null
alter table raw.Salesforce_Case add ContactEmail [varchar](255) null
alter table raw.Salesforce_Case add ContactFax [nvarchar](50) null
alter table raw.Salesforce_Case add ContactMobile [nvarchar](50) null
alter table raw.Salesforce_Case add ContactName__c [nvarchar](50) null
alter table raw.Salesforce_Case add ContactPhone [nvarchar](50) null
alter table raw.Salesforce_Case add Contact_FirstName__c [nvarchar](50) null
alter table raw.Salesforce_Case add Customer_type__c [varchar](255) null
alter table raw.Salesforce_Case add End_Stage_Check_up__c [datetime] null
alter table raw.Salesforce_Case add End_Stage_Closing__c [datetime] null
alter table raw.Salesforce_Case add End_Stage_New__c [datetime] null
alter table raw.Salesforce_Case add End_Stage_Welcome__c [datetime] null
alter table raw.Salesforce_Case add Number_of_outbound__c [float] null
alter table raw.Salesforce_Case add Planned_go_live__c [datetime] null
alter table raw.Salesforce_Case add Reason_for_no_consult__c [nvarchar](max) null
alter table raw.Salesforce_Case add Stage_Closed__c [datetime] null
alter table raw.Salesforce_Case add Stage_Phase__c [varchar](10) null