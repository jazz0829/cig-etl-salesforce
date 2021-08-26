using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Task
{
	public sealed class TaskCsvTransformer : CsvToDataTableTransformer<Model.Task>, ISalesforceObjectTransformer
	{
		public DataTable Transform(string data, DateTime insertTime)
		{
			return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<TaskMap>);
		}
	}
}
