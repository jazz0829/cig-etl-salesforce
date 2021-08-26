using System;
using System.Globalization;
using Eol.Cig.Etl.Salesforce.Transform.Account;
using Eol.Cig.Etl.Salesforce.Transform.Case;
using Eol.Cig.Etl.Salesforce.Transform.CaseComment;
using Eol.Cig.Etl.Salesforce.Transform.ClassroomTraining;
using Eol.Cig.Etl.Salesforce.Transform.CaseHistory;
using Eol.Cig.Etl.Salesforce.Transform.Contact;
using Eol.Cig.Etl.Salesforce.Transform.CustomerNextBestAction;
using Eol.Cig.Etl.Salesforce.Transform.IndividualEmailResult;
using Eol.Cig.Etl.Salesforce.Transform.KeyEvent;
using Eol.Cig.Etl.Salesforce.Transform.LiveAgentsession;
using Eol.Cig.Etl.Salesforce.Transform.LiveChatButton;
using Eol.Cig.Etl.Salesforce.Transform.LiveChatButtonSkill;
using Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscript;
using Eol.Cig.Etl.Salesforce.Transform.LiveChatTranscriptEvent;
using Eol.Cig.Etl.Salesforce.Transform.LiveChatVisitor;
using Eol.Cig.Etl.Salesforce.Transform.RecordType;
using Eol.Cig.Etl.Salesforce.Transform.SkillProfile;
using Eol.Cig.Etl.Salesforce.Transform.SkillUser;
using Eol.Cig.Etl.Salesforce.Transform.Survey;
using Eol.Cig.Etl.Salesforce.Transform.Task;
using Eol.Cig.Etl.Salesforce.Transform.Topic;
using Eol.Cig.Etl.Salesforce.Transform.TopicAssignment;
using Eol.Cig.Etl.Salesforce.Transform.User;
using Eol.Cig.Etl.Salesforce.Transform.WebActivity;

namespace Eol.Cig.Etl.Salesforce.Transform
{
    public class SalesforceObjectTransformerFactory : ISalesforceObjectTransformerFactory
    {
        public ISalesforceObjectTransformer CreateTransformer(string salesforceObject)
        {
            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Contact.ObjectName)
            {
                return new ContactCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Account.ObjectName)
            {
                return new AccountCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.CaseComment.ObjectName)
            {
                return new CaseCommentCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Case.ObjectName)
            {
                return  new CaseCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.RecordType.ObjectName)
            {
                return new RecordTypeCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.User.ObjectName)
            {
                return new UserCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Survey.ObjectName)
            {
                return new SurveyCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Topic.ObjectName)
            {
                return new TopicCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.TopicAssignment.ObjectName)
            {
                return new TopicAssignmentCsvTransformer();
            }

            if(salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.CustomerNextBestAction.ObjectName)
            {
                return new CustomerNextBestActionCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveAgentSession.ObjectName)
            {
                return new LiveAgentSessionCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveChatButton.ObjectName)
            {
                return new LiveChatButtonCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveChatButtonSkill.ObjectName)
            {
                return new LiveChatButtonSkillCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveChatTranscript.ObjectName)
            {
                return new LiveChatTranscriptCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveChatTranscriptEvent.ObjectName)
            {
                return new LiveChatTranscriptEventCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.LiveChatVisitor.ObjectName)
            {
                return new LiveChatVisitorCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.SkillUser.ObjectName)
            {
                return new SkillUserCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.SkillProfile.ObjectName)
            {
                return new SkillProfileCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.KeyEvent.ObjectName)
            {
                return new KeyEventCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.WebActivity.ObjectName)
            {
                return new WebActivityCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.IndividualEmailResult.ObjectName)
            {
                return new IndividualEmailResultCsvTransformer();
            }

            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.ClassroomTraining.ObjectName)
            {
                return new ClassroomTrainingCsvTransformer();
            }
            
            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.Task.ObjectName)
            {
                return new TaskCsvTransformer();
            }
            
            if (salesforceObject.ToUpper(CultureInfo.InvariantCulture) == Model.CaseHistory.ObjectName)
            {
                return new CaseHistoryCsvTransformer();
            }
            
            throw new ArgumentException($"Missing implementation for salesforce object {salesforceObject}");
        }
    }
}
