using System.Threading.Tasks;
using Eol.Cig.Etl.Salesforce.Configuration;
using Eol.Cig.Etl.Salesforce.Configuration.Common;
using Eol.Cig.Etl.Salesforce.Extract;
using Eol.Cig.Etl.Salesforce.Transform;
using Eol.Cig.Etl.Shared.Load;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Jobs.Export
{
    public class LiveChatTranscriptExport: ExportJob
    {
        public static readonly string Name = nameof(LiveChatTranscriptExport);

        public LiveChatTranscriptExport(ILog logger, IAwsConfiguration awsConfiguration,
            ISalesforceConfiguration jobConfiguration,
            ISalesforceObjectTransformerFactory objectTransformerFactory,
            ISalesforceExtractorFactory salesforceExtractorFactory,
            ISqlServerUploaderFactory sqlServerUploaderFactory)
            : base(logger, awsConfiguration, jobConfiguration, objectTransformerFactory, salesforceExtractorFactory, sqlServerUploaderFactory)
        { }

        public override async Task Execute()
        {
            await base.Execute();
        }

        public override string GetName()
        {
            return Name;
        }
    }
}
