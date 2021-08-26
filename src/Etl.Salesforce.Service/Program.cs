using Eol.Cig.Etl.Shared.Service;
using Eol.Cig.Etl.Salesforce.Service.Configuration;
using Quartz;
using Topshelf;
using Topshelf.Quartz.StructureMap;
using Topshelf.StructureMap;
using Eol.Cig.Etl.Salesforce.Jobs;
using Eol.Cig.Etl.Shared.Extensions;
using Eol.Cig.Etl.Salesforce.Jobs.Upload;
using Eol.Cig.Etl.Salesforce.Jobs.Export;

namespace Eol.Cig.Etl.Salesforce.Service
{
    static class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(c =>
            {
                var container = Bootstrapper.CreateContainer();
                c.UseStructureMap(container);

                var configuration = container.GetInstance<ISalesforceServiceConfiguration>();
                c.ConfigureHost(configuration);

                c.Service<IEtlService>(s =>
                {
                    s.ConfigureService();

                    var webActivityExportJob = JobBuilder.Create<QuartzEtlJob<WebActivityExport>>().WithIdentity(WebActivityExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(webActivityExportJob, configuration.Schedule));

                    var keyEventExportJob = JobBuilder.Create<QuartzEtlJob<KeyEventExport>>().WithIdentity(KeyEventExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(keyEventExportJob, configuration.Schedule));

                    var caseExportJobDetails = JobBuilder.Create<QuartzEtlJob<CaseExport>>().WithIdentity(CaseExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(caseExportJobDetails, configuration.TwiceADaySchedule));

                    var accountExportJobDetails = JobBuilder.Create<QuartzEtlJob<AccountExport>>().WithIdentity(AccountExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(accountExportJobDetails, configuration.Schedule));

                    var caseCommentExportJobDetails = JobBuilder.Create<QuartzEtlJob<CaseCommentExport>>().WithIdentity(CaseCommentExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(caseCommentExportJobDetails, configuration.Schedule));

                    var contactExportJobDetails = JobBuilder.Create<QuartzEtlJob<ContactExport>>().WithIdentity(ContactExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(contactExportJobDetails, configuration.Schedule));

                    var recordTypeExportJobDetails = JobBuilder.Create<QuartzEtlJob<RecordTypeExport>>().WithIdentity(RecordTypeExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(recordTypeExportJobDetails, configuration.Schedule));

                    var userExportJobDetails = JobBuilder.Create<QuartzEtlJob<UserExport>>().WithIdentity(UserExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(userExportJobDetails, configuration.Schedule));

                    var surveyExportJobDetails = JobBuilder.Create<QuartzEtlJob<SurveyExport>>().WithIdentity(SurveyExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(surveyExportJobDetails, configuration.Schedule));

                    var topicAssignmentJobDetails = JobBuilder.Create<QuartzEtlJob<TopicAssignmentExport>>().WithIdentity(TopicAssignmentExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(topicAssignmentJobDetails, configuration.Schedule));

                    var topicJobDetails = JobBuilder.Create<QuartzEtlJob<TopicExport>>().WithIdentity(TopicExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(topicJobDetails, configuration.Schedule));

                    var knowledgeArticleVersionExportJobDetails = JobBuilder.Create<QuartzEtlJob<KnowledgeArticleVersionExport>>().WithIdentity(KnowledgeArticleVersionExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(knowledgeArticleVersionExportJobDetails, configuration.Schedule));

                    var customerNextBestActionExportDetails = JobBuilder.Create<QuartzEtlJob<CustomerNextBestActionExport>>().WithIdentity(CustomerNextBestActionExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(customerNextBestActionExportDetails, configuration.Schedule));

                    //var liveAgentSessionExportDetails = JobBuilder.Create<QuartzEtlJob<LiveAgentSessionExport>>().WithIdentity(LiveAgentSessionExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveAgentSessionExportDetails, configuration.Schedule));

                    //var liveChatButtonExportDetails = JobBuilder.Create<QuartzEtlJob<LiveChatButtonExport>>().WithIdentity(LiveChatButtonExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveChatButtonExportDetails, configuration.Schedule));


                    //var liveChatButtonSkillExportDetails = JobBuilder.Create<QuartzEtlJob<LiveChatButtonSkillExport>>().WithIdentity(LiveChatButtonSkillExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveChatButtonSkillExportDetails, configuration.Schedule));

                    var liveChatTranscriptExportDetails = JobBuilder.Create<QuartzEtlJob<LiveChatTranscriptExport>>().WithIdentity(LiveChatTranscriptExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveChatTranscriptExportDetails, configuration.Schedule));

                    //var liveChatTranscriptEventExportDetails = JobBuilder.Create<QuartzEtlJob<LiveChatTranscriptEventExport>>().WithIdentity(LiveChatTranscriptEventExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveChatTranscriptEventExportDetails, configuration.Schedule));

                    //var liveChatVisitorExportDetails = JobBuilder.Create<QuartzEtlJob<LiveChatVisitorExport>>().WithIdentity(LiveChatVisitorExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(liveChatVisitorExportDetails, configuration.Schedule));

                    //var skillUserExportDetails = JobBuilder.Create<QuartzEtlJob<SkillUserExport>>().WithIdentity(SkillUserExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(skillUserExportDetails, configuration.Schedule));

                    //var skillProfileExportDetails = JobBuilder.Create<QuartzEtlJob<SkillProfileExport>>().WithIdentity(SkillProfileExport.Name).Build();
                    //s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(skillProfileExportDetails, configuration.Schedule));

                    var individualEmailResultExportDetails = JobBuilder.Create<QuartzEtlJob<IndividualEmailResultExport>>().WithIdentity(IndividualEmailResultExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(individualEmailResultExportDetails, configuration.Schedule));

                    var classroomTrainingExportDetails = JobBuilder.Create<QuartzEtlJob<ClassroomTrainingExport>>().WithIdentity(ClassroomTrainingExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(classroomTrainingExportDetails, configuration.Schedule));

                    var taskExportDetails = JobBuilder.Create<QuartzEtlJob<TaskExport>>().WithIdentity(TaskExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(taskExportDetails, configuration.Schedule));

                    var caseHistoryExportDetails = JobBuilder.Create<QuartzEtlJob<CaseHistoryExport>>().WithIdentity(CaseHistoryExport.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(caseHistoryExportDetails, configuration.Schedule));

                    var heartbeatJobDetails = JobBuilder.Create<QuartzEtlJob<Heartbeat>>().WithIdentity(Heartbeat.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(heartbeatJobDetails, configuration.Schedule));

                    var healthScoreUploadJob = JobBuilder.Create<QuartzEtlJob<HealthScoreUpload>>().WithIdentity(HealthScoreUpload.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(healthScoreUploadJob, configuration.GeneralUploadSchedule));

                    var customerNextBestActionsUploadJob = JobBuilder.Create<QuartzEtlJob<CustomerNextBestActionsUpload>>().WithIdentity(CustomerNextBestActionsUpload.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(customerNextBestActionsUploadJob, configuration.GeneralUploadSchedule));

                    var caseReasonsUploadJob = JobBuilder.Create<QuartzEtlJob<CaseReasonsUpload>>().WithIdentity(CaseReasonsUpload.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(caseReasonsUploadJob, configuration.CaseReasonsUploadSchedule));

                    var keyEventsUploadJob = JobBuilder.Create<QuartzEtlJob<KeyEventsUpload>>().WithIdentity(KeyEventsUpload.Name).Build();
                    s.ScheduleQuartzJob(q => q.ConfigureJobWithSchedule(keyEventsUploadJob, configuration.KeyEventsUploadSchedule));
                });
            });

            host.Run();
        }
    }
}
