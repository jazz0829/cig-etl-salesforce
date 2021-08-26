using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.CaseHistory
{
	public sealed class CaseHistoryCsvTransformer : CsvToDataTableTransformer<Model.CaseHistory>, ISalesforceObjectTransformer
	{
		public DataTable Transform(string data, DateTime insertTime)
		{
			return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<CaseHistoryMap>);
		}
	}
}
