using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class Topic: SalesforceObject
    {
        public static readonly string ObjectName = "TOPIC";

        public static readonly string Query =
            "SELECT Id, TalkingAbout, SystemModstamp, Name, Description, CreatedDate, CreatedById FROM Topic";

        public string Id { get; set; }
        public int TalkingAbout { get; set; }
        public DateTime SystemModstamp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime LastModifiedDate => SystemModstamp;
    }
}
