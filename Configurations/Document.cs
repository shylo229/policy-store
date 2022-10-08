using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace policystore.Configurations
{

    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        [BsonElement("date_created")]
        DateTime DateCreated { get; }
    }

    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("date_created")]
        public DateTime DateCreated => DateTime.Now;
    }
}
