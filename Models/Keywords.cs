using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("keywords")]
    [GraphQLDescription("Includes multiple keywords for the movie.")]
    public class Keywords
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }

        [BsonElement("keywords")]
        [GraphQLName("keywords")]
        public Words[]? Words { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Words
    {
        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string? Name { get; set; }
    }
}
