using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("credits")]
    public class Credits
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }
        
        [BsonElement("cast")]
        [GraphQLName("cast")]
        public Cast[] Cast { get; set; }

        [BsonElement("crew")]
        [GraphQLName("crew")]
        public Crew[] Crew { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Cast
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }

        [BsonElement("order")]
        [GraphQLName("order")]
        public int order { get; set; }

        [BsonElement("cast_id")]
        [GraphQLName("castid")]
        public int CastId { get; set; }

        [BsonElement("credit_id")]
        [GraphQLName("creditid")]
        public string CreditId { get; set; }

        [BsonElement("character")]
        [GraphQLName("character")]
        public string Character { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }

        [BsonElement("original_name")]
        [GraphQLName("originalname")]
        public string OriginalName { get; set; }

        [BsonElement("profile_path")]
        [GraphQLName("picture")]
        public string? ProfilePath { get; set; }

        [BsonElement("popularity")]
        [GraphQLName("popularity")]
        public double Popularity { get; set; }

        [BsonElement("adult")]
        [GraphQLName("adult")]
        public bool IsAdult { get; set; }

        [BsonElement("gender")]
        [GraphQLName("gender")]
        public int Gender { get; set; }

        [BsonElement("known_for_department")]
        [GraphQLName("knownfordepartment")]
        public string KnownForDepartment { get; set; }

    }

    [BsonIgnoreExtraElements]

    public class Crew
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }

        [BsonElement("credit_id")]
        [GraphQLName("creditid")]
        public string CreditId { get; set; }

        [BsonElement("department")]
        [GraphQLName("department")]
        public string Department { get; set; }

        [BsonElement("job")]
        [GraphQLName("job")]
        public string Job { get; set; }

        [BsonElement("adult")]
        [GraphQLName("adult")]
        public bool IsAdult { get; set; }

        [BsonElement("gender")]
        [GraphQLName("gender")]
        public int Gender { get; set; }

        [BsonElement("known_for_department")]
        [GraphQLName("knownfordepartment")]
        public string KnownForDepartment { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }

        [BsonElement("original_name")]
        [GraphQLName("originalname")]
        public string OriginalName { get; set; }

        [BsonElement("profile_path")]
        [GraphQLName("picture")]
        public string? ProfilePath { get; set; }

        [BsonElement("popularity")]
        [GraphQLName("popularity")]
        public double Popularity { get; set; }

    }
}
