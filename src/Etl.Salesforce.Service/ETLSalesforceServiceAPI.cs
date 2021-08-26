using System;
using log4net;
using Eol.Cig.Etl.Shared.Service;
using Eol.Cig.Etl.Salesforce.Service.Configuration;

namespace Eol.Cig.Etl.Salesforce.Service
{
	public class EtlSalesforceServiceApi: EtlServiceApi, IEtlSalesfoceServiceApi
	{
	    public EtlSalesforceServiceApi(ISalesforceServiceConfiguration configuration, ILog logger) : base(configuration)
		{
		}

		public override string Execute()
		{

			return "Not implemented";
		}

		public string Execute(DateTime startTime, DateTime endTime)
		{

			return "Not implemented";
		}
	}
}
