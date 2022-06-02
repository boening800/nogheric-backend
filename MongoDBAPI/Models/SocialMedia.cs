using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBAPI.Models
{
    public class SocialMedia
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
}
