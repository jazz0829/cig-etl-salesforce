using Eol.Cig.Etl.Shared.Api;
using Eol.Cig.Etl.Shared.Service;
using Eol.Cig.Etl.Salesforce.Service.Configuration;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Service
{
	internal class EtlSalesforceService: EtlService<IEtlSalesfoceServiceApi, EtlSalesforceServiceApi>
	{
		public EtlSalesforceService(ISalesforceServiceConfiguration configuration, ILog logger, IHostedApiFactory hostedApiFactory)
			: base(configuration, logger, hostedApiFactory)
		{
		}

		protected override void LogSchedule()
		{
			// Do Nothing
		}
	}
}
