using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eol.Cig.Etl.Shared.Service;

namespace Eol.Cig.Etl.Salesforce.Service
{
	[ServiceContract]
	public interface IEtlSalesfoceServiceApi: IEtlServiceApi
	{
		[OperationContract(Name = "ExecuteByStartEndTime")]
		[WebGet]
		string Execute(DateTime startTime, DateTime endTime);
	}
}
