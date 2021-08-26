using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.ClassroomTraining
{
	public sealed class ClassroomTrainingCsvTransformer : CsvToDataTableTransformer<Model.ClassroomTraining>, ISalesforceObjectTransformer
	{
		public DataTable Transform(string data, DateTime insertTime)
		{
			return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<ClassroomTrainingTransformerMap>);
		}
	}
}
