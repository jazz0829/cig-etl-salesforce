using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
	public class CaseHistory : SalesforceObject
	{
		public static readonly string ObjectName = "CASEHISTORY";
		public static readonly string Query = @"SELECT CaseId,CreatedById,CreatedDate,Field,Id,IsDeleted,NewValue,OldValue FROM CaseHistory";

		public string Id { get; set; }
		public string CaseId { get; set; }
		public string Field { get; set; }
		public bool? IsDeleted { get; set; }
		public string NewValue { get; set; }
		public string OldValue { get; set; }
		public string CreatedById { get; set; }
		public DateTime? CreatedDate { get; set; }

	}
}
