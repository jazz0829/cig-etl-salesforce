namespace Eol.Cig.Etl.Salesforce.Client
{
    public class CreateAttachmentBatchRequest : CreateBatchRequest
    {
        public string FilePath { get; set; }

        public string ParentId { get; set; }
    }
}
