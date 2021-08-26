using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class TopicAssignment: SalesforceObject
    {
        public static readonly string ObjectName = "TOPICASSIGNMENT";

        public static readonly string Query =
            "SELECT EntityId, EntityKeyPrefix, EntityType, Id, IsDeleted, NetworkId, SystemModstamp, TopicId, CreatedById, CreatedDate FROM TopicAssignment";

        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EntityId { get; set; }
        public string EntityKeyPrefix { get; set; }
        public string EntityType { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string NetworkId { get; set; }
        public DateTime SystemModstamp { get; set; }
        public string TopicId { get; set; }

        public DateTime LastModifiedDate => SystemModstamp;
    }
}
