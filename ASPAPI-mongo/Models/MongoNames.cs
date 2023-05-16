using MongoDB.Bson.Serialization.Attributes;

using MongoDB.Bson;

namespace ASPAPI_mongo.Models

{
    public class MongoNames
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        //[BsonElement("name")]
        public string name { get; set; } = null!;
        public string gender1 { get; set; } = null!;
        public string usage { get; set; } = null!;
        public int yearMost { get; set; }
        public int yearLeast { get; set; }
      

    }
}
