CREATE VIEW [raw].[Salesforce_Account_Latest]
AS
	WITH Salesforce_Account_Latest
	(Id, AccountNumber, LastModifiedDate, EtlInsertTime, Exact_ID__c, RN)
	AS
	(
		SELECT Id, AccountNumber, LastModifiedDate, EtlInsertTime, Exact_ID__c,
			ROW_NUMBER() OVER (PARTITION BY Id ORDER BY LastModifiedDate DESC) AS RN
		FROM [raw].[Salesforce_Account]
	)

	SELECT Id, AccountNumber, LastModifiedDate, EtlInsertTime, Exact_ID__c
	FROM Salesforce_Account_Latest
	WHERE RN = 1
GO

CREATE VIEW [raw].[Salesforce_Case_Latest]
AS
	WITH Salesforce_Case_Latest
	(Id, AccountId, CaseNumber, ClosedDate, CreatedDate, Description, IsClosed, IsDeleted, IsEscalated, LastModifiedDate, LastReferencedDate, LastViewedDate, Origin, Priority, Reason, RecordTypeId, Status, Subject, Type, CreatedById, LastModifiedById, OwnerId, Account_Number__c, Call_me_back__c, Case_subject__c, Date_Time_Answered__c, Enddate__c, Exact_Creation_Date__c, Exact_ID__c, Exact_Request_Number__c, Main_Reason__c, Planned_End_Date__c, Reject_Reason__c, Solution__c, Startdate__c, Start_Date__c, Sub_Reason__c, Owner_Role__c, Related_case__c, Account_Hosting_Environment__c, EtlInsertTime, ContactId, RN)
	AS
	(
		SELECT Id, AccountId, CaseNumber, ClosedDate, CreatedDate, Description, IsClosed, IsDeleted, IsEscalated, LastModifiedDate, LastReferencedDate, LastViewedDate, Origin, Priority, Reason, RecordTypeId, Status, Subject, Type, CreatedById, LastModifiedById, OwnerId, Account_Number__c, Call_me_back__c, Case_subject__c, Date_Time_Answered__c, Enddate__c, Exact_Creation_Date__c, Exact_ID__c, Exact_Request_Number__c, Main_Reason__c, Planned_End_Date__c, Reject_Reason__c, Solution__c, Startdate__c, Start_Date__c, Sub_Reason__c, Owner_Role__c, Related_case__c, Account_Hosting_Environment__c, EtlInsertTime, ContactId,
			ROW_NUMBER() OVER (PARTITION BY Id ORDER BY LastModifiedDate DESC) AS RN
		FROM [raw].[Salesforce_Case]
	)

	SELECT Id, AccountId, CaseNumber, ClosedDate, CreatedDate, Description, IsClosed, IsDeleted, IsEscalated, LastModifiedDate, LastReferencedDate, LastViewedDate, Origin, Priority, Reason, RecordTypeId, Status, Subject, Type, CreatedById, LastModifiedById, OwnerId, Account_Number__c, Call_me_back__c, Case_subject__c, Date_Time_Answered__c, Enddate__c, Exact_Creation_Date__c, Exact_ID__c, Exact_Request_Number__c, Main_Reason__c, Planned_End_Date__c, Reject_Reason__c, Solution__c, Startdate__c, Start_Date__c, Sub_Reason__c, Owner_Role__c, Related_case__c, Account_Hosting_Environment__c, EtlInsertTime, ContactId
	FROM Salesforce_Case_Latest
	WHERE RN = 1
GO

CREATE VIEW [raw].[Salesforce_CaseComment_Latest]
AS
	WITH Salesforce_CaseComment_Latest
	(Id, CommentBody, CreatedById, CreatedDate, IsDeleted, IsPublished, LastModifiedById, LastModifiedDate, ParentId, EtlInsertTime, RN)
	AS
	(
		SELECT Id, CommentBody, CreatedById, CreatedDate, IsDeleted, IsPublished, LastModifiedById, LastModifiedDate, ParentId, EtlInsertTime,
			ROW_NUMBER() OVER (PARTITION BY Id ORDER BY LastModifiedDate DESC) AS RN
		FROM [raw].[Salesforce_CaseComment]
	)

	SELECT Id, CommentBody, CreatedById, CreatedDate, IsDeleted, IsPublished, LastModifiedById, LastModifiedDate, ParentId, EtlInsertTime
	FROM Salesforce_CaseComment_Latest
	WHERE RN = 1
GO

CREATE VIEW [raw].[Salesforce_Contact_Latest]
AS
	WITH Salesforce_Contact_Latest
	(Id, FirstName, LastName, AccountId, CreatedDate, IsDeleted, IsEmailBounced, LastActivityDate, LastCURequestDate, LastCUUpdateDate, LastModifiedDate, LastViewedDate, Enddate__c, UserID__c, EOL_User__c, Exact_ID__c, Gender__c, Language__c, Startdate__c, EtlInsertTime, RN)
	AS 
	(
		SELECT Id, FirstName, LastName, AccountId, CreatedDate, IsDeleted, IsEmailBounced, LastActivityDate, LastCURequestDate, LastCUUpdateDate, LastModifiedDate, LastViewedDate, Enddate__c, UserID__c, EOL_User__c, Exact_ID__c, Gender__c, Language__c, Startdate__c, EtlInsertTime,
			ROW_NUMBER() OVER (PARTITION BY Id ORDER BY LastModifiedDate DESC) AS RN
		FROM [raw].Salesforce_Contact
	)

	SELECT  Id, FirstName, LastName, AccountId, CreatedDate, IsDeleted, IsEmailBounced, LastActivityDate, LastCURequestDate, LastCUUpdateDate, LastModifiedDate, LastViewedDate, Enddate__c, UserID__c, EOL_User__c, Exact_ID__c, Gender__c, Language__c, Startdate__c, EtlInsertTime
	FROM Salesforce_Contact_Latest
	where RN = 1

GO

CREATE VIEW [raw].[Salesforce_RecordType_Latest]
AS
	WITH Salesforce_RecordType_Latest
	(Id, Name, Description, IsActive, BusinessProcessId, CreatedById, CreatedDate, DeveloperName, LastModifiedById, LastModifiedDate, NamespacePrefix, SobjectType, SystemModstamp, EtlInsertTime, RN)
	AS
	(
		SELECT Id, Name, Description, IsActive, BusinessProcessId, CreatedById, CreatedDate, DeveloperName, LastModifiedById, LastModifiedDate, NamespacePrefix, SobjectType, SystemModstamp, EtlInsertTime,
			ROW_NUMBER() OVER (PARTITION BY Id ORDER BY LastModifiedDate DESC) AS RN
		FROM [raw].[Salesforce_RecordType]
	)

	SELECT Id, Name, Description, IsActive, BusinessProcessId, CreatedById, CreatedDate, DeveloperName, LastModifiedById, LastModifiedDate, NamespacePrefix, SobjectType, SystemModstamp, EtlInsertTime
	FROM Salesforce_RecordType_Latest
	WHERE RN = 1
GO

CREATE VIEW [domain].[SupportCases]
AS
SELECT
	CA.CaseNumber, 
	AC.Exact_ID__c AS AccountID,
	CA.Account_Hosting_Environment__c AS Environment,
	AC.AccountNumber AS AccountCode,	
	CO.UserID__c AS UserID,
	CA.Origin AS Origin,
	Cast(CA.CreatedDate AS Date) AS [Opened Date], 
	CA.CreatedDate AS [Date/Time Opened],
	Cast(CA.ClosedDate AS Date) AS [Closed Date], 
	CA.ClosedDate AS [Date/Time Closed],
	RT.Name AS [Case Record Type],       
	CA.Type AS [Type],
	CA.Main_Reason__c AS [Main Reason],
	CA.Sub_Reason__c AS [Sub Reason],
	CA.Subject AS [Subject],
	CA.Description AS [Description],
	CA.Solution__c AS [Solution]

FROM [raw].Salesforce_Case_Latest CA

LEFT JOIN [raw].Salesforce_Account_Latest AC
ON CA.AccountId=AC.Id

LEFT JOIN [raw].Salesforce_Contact_Latest CO
ON CA.ContactId=CO.Id

LEFT JOIN [raw].Salesforce_RecordType_Latest RT
ON CA.RecordTypeId=RT.Id

GO