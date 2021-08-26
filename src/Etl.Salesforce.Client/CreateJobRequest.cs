namespace Eol.Cig.Etl.Salesforce.Client
{
	public class CreateJobRequest
	{
		public static CreateJobRequest CreateQueryJob(string objectToQuery)
		{
			var job = new CreateJobRequest
			{
				ContentType = JobContentType.Csv,
				Operation = JobOperation.Query,
				Object = objectToQuery
			};
			return job;
		}

		public JobOperation Operation { get; set; }

		internal string OperationString
		{
			get
			{
				switch (Operation)
				{
					case JobOperation.Insert:
						return "insert";
					case JobOperation.Update:
						return "update";
					case JobOperation.HardDelete:
						return "hardDelete";
					case JobOperation.Delete:
						return "delete";
					case JobOperation.Query:
						return "query";
					default:
						return "upsert";
				}
			}
		}

		public string Object { get; set; }
		public JobContentType ContentType { get; set; }
		public string ExternalIdFieldName { get; set; }
		internal string ContentTypeString
		{
			get
			{
				switch (ContentType)
				{
					case JobContentType.Csv:
						return "CSV";
					case JobContentType.ZipCsv:
						return "ZIP_CSV";
					default:
						return "XML";
				}
			}
		}
	}
}
