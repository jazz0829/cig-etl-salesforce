using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class KnowledgeArticleVersion: SalesforceObject
    {
        public static readonly string ObjectName = "KNOWLEDGEARTICLEVERSION";

        public static readonly string Query = @"SELECT CreatedDate, ArticleCreatedDate,ArticleNumber,ArticleType,FirstPublishedDate,Id,Language," +
                                              "LastPublishedDate,PublishStatus,SourceId,Summary,Title,UrlName FROM KnowledgeArticleVersion";

        public DateTime? CreatedDate { get; set; }
        public DateTime? ArticleCreatedDate { get; set; }
        public DateTime? FirstPublishedDate { get; set; }
        public DateTime? LastPublishedDate { get; set; }
        public string PublishStatus { get; set; }
        public string Summary { get; set; }
        public string UrlName { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleType { get; set; }
        public string Id { get; set; }
        public string SourceId { get; set; }
        public DateTime? LastModifiedDate => CreatedDate;
    }
}
