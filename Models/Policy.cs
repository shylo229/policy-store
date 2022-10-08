using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using policystore.Configurations;

namespace policystore.Models
{
    [BsonCollection("policy")]
    public class Policy : Document
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("content")]
        public string Content { get; set; }
    }
}
