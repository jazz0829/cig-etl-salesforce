using System.Configuration;
using Eol.Cig.Etl.Shared.Configuration;
using Eol.Cig.Etl.Shared.Load;
using Eol.Cig.Etl.Shared.Service;
using Eol.Cig.Etl.Shared.Utils;
using Eol.Cig.Etl.Salesforce.Client;
using Eol.Cig.Etl.Salesforce.Configuration;
using Eol.Cig.Etl.Salesforce.Extract;
using Eol.Cig.Etl.Salesforce.Upload;
using Eol.Cig.Etl.Salesforce.Load;
using Eol.Cig.Etl.Salesforce.Service.Configuration;
using Eol.Cig.Etl.Salesforce.Transform;
using StructureMap;
using Eol.Cig.Etl.Salesforce.Jobs;
using Eol.Cig.Etl.Salesforce.Configuration.Upload;
using Eol.Cig.Etl.Salesforce.Jobs.Export;
using Eol.Cig.Etl.Salesforce.Jobs.Upload;
using Eol.Cig.Etl.Salesforce.Configuration.Common;

namespace Eol.Cig.Etl.Salesforce.Service
{
    public static class Bootstrapper
    {
        static readonly ISalesforceServiceConfiguration ServiceConfiguration = new SalesforceServiceConfiguration(ConfigurationManager.AppSettings);
        static readonly ISalesforceConfiguration Configuration = new SalesforceConfiguration(ConfigurationManager.GetSection("salesforceConfiguration")
            as SalesforceConfigurationSection, ConfigurationManager.ConnectionStrings);

        static readonly IAwsConfiguration awsConfig = new AwsConfiguration();

        static readonly ISalesforceUploadConfiguration UploadConfiguration = new SalesforceUploadConfiguration(ConfigurationManager.GetSection("salesforceUploadConfiguration")
    as SalesforceUploadConfigurationSection, ConfigurationManager.ConnectionStrings);


        public static IContainer CreateContainer()
        {
            var parentContainer = BootstrapperUtils.CreateDefaultContainer(ServiceConfiguration);
            var childContainer = parentContainer.CreateChildContainer();

            childContainer.Configure(cfg =>
            {
                cfg.For<IEtlService>().Use<EtlSalesforceService>().Singleton();
                cfg.For<IEtlSalesfoceServiceApi>().Use<EtlSalesforceServiceApi>().Singleton();
                cfg.For<IServiceConfiguration>().Use(ServiceConfiguration).Singleton();
                cfg.For<ISalesforceServiceConfiguration>().Use(ServiceConfiguration).Singleton();
                cfg.For<ISalesforceConfiguration>().Use(Configuration).Singleton();
                cfg.For<ISalesforceUploadConfiguration>().Use(UploadConfiguration).Singleton();

                cfg.For<IAwsConfiguration>().Use(awsConfig).Singleton();


                cfg.For<ISalesforceExtractorFactory>().Use<SalesforceExtractorFactory>().Singleton();
                cfg.For<ISalesforceUploaderFactory>().Use<SalesforceUploaderFactory>().Singleton();
                cfg.For<ISqlServerUploaderFactory>().Use<SqlServerUploaderFactory>();
                cfg.For<ISalesforceObjectTransformerFactory>().Use<SalesforceObjectTransformerFactory>();

                foreach (var jobConfig in Configuration.Jobs)
                {
                    cfg.For<ISqlJobConfiguration>().Add(jobConfig.Value).Named(jobConfig.Key);
                    cfg.For<ISqlServerUploader>()
                        .Add<SalesforceSqlServerUploader>()
                        .Named(jobConfig.Key)
                        .Ctor<ISqlJobConfiguration>()
                        .IsNamedInstance(jobConfig.Key);
                }

                foreach (var jobConfig in UploadConfiguration.Jobs)
                {
                    cfg.For<ISalesforceUploadJobConfiguration>().Add(jobConfig.Value).Named(jobConfig.Key);
                    cfg.For<ISqlServerUploader>()
                        .Add<SalesforceSqlServerUploader>()
                        .Named(jobConfig.Key)
                        .Ctor<ISalesforceUploadJobConfiguration>()
                        .IsNamedInstance(jobConfig.Key);
                }


                cfg.For<IEtlJob>().AddInstances(i =>
                {
                    i.Type<AccountExport>();
                    i.Type<CaseCommentExport>();
                    i.Type<CaseExport>();
                    i.Type<WebActivityExport>();
                    i.Type<KeyEventExport>();
                    i.Type<ContactExport>();
                    i.Type<RecordTypeExport>();
                    i.Type<UserExport>();
                    i.Type<SurveyExport>();
                    i.Type<TopicExport>();
                    i.Type<TopicAssignmentExport>();
                    i.Type<KnowledgeArticleVersionExport>();
                    i.Type<CustomerNextBestActionExport>();
                    i.Type<LiveAgentSessionExport>();
                    i.Type<LiveChatButtonExport>();
                    i.Type<LiveChatButtonSkillExport>();
                    i.Type<LiveChatTranscriptExport>();
                    i.Type<LiveChatTranscriptEventExport>();
                    i.Type<LiveChatVisitorExport>();
                    i.Type<SkillUserExport>();
                    i.Type<SkillProfileExport>();
                    i.Type<HealthScoreUpload>();
                    i.Type<CustomerNextBestActionsUpload>();
                    i.Type<CaseReasonsUpload>();
                    i.Type<IndividualEmailResultExport>();
                    i.Type<ClassroomTrainingExport>();
                    i.Type<TaskExport>();
                    i.Type<CaseHistoryExport>();
                    i.Type<Heartbeat>();
                });
            });

            return childContainer;
        }

    }
}
