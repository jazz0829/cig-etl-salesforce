namespace Eol.Cig.Etl.Salesforce.Client
{
    public class CreateBatchRequest
    {
        public string JobId { get; set; }

        public string BatchContents { get; set; }

        public BatchContentType? ContentType { get; set; }

        internal string BatchContentHeader => ContentType.HasValue && ContentType.Value == BatchContentType.Xml ? "application/xml" : "text/csv";
    }
}
