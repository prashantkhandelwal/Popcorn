using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("person")]
    [GraphQLDescription("Contains information about people.")]
    public class Person
    {
        private DateTime? _birthDate;
        private DateTime? _deathDate;

        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        [GraphQLDescription("TMDB ID of the person.")]
        public int Id { get; set; }

        [BsonElement("imdb_id")]
        [GraphQLName("imdbid")]
        [GraphQLDescription("IMDB Person Id. This is the unique ID for person listed in IMDB.com. The ID is in string format. E.g.: nm0000206")]
        public string IMDBId { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        [GraphQLDescription("Name of the person.")]
        public string Name { get; set; }

        [BsonElement("also_known_as")]
        [GraphQLName("alsoknownas")]
        [GraphQLDescription("Alternate known names of the person.")]
        public string[] AlsoKnownAs { get; set; }

        [BsonElement("biography")]
        [GraphQLName("bio")]
        [GraphQLDescription("Biography of the person.")]
        public string Bio { get; set; }

        [BsonElement("adult")]
        [GraphQLName("isadult")]
        [GraphQLDescription("If person is an adult actor.")]
        public bool IsAdult { get; set; }

        [BsonElement("birthday")]
        [GraphQLName("birthday")]
        [GraphQLDescription("Birthday of the person.")]
        public DateTime? BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value.ToString(), out dt))
                {
                    if (dt.ToString() == "01-01-1970 00:00:00")
                    {
                        _birthDate = null;
                    }
                    else
                    {
                        _birthDate = Convert.ToDateTime(value);
                    }
                }
                else
                {
                    _birthDate = null;
                }
            }
        }

        [BsonElement("place_of_birth")]
        [GraphQLName("placeofbirth")]
        [GraphQLDescription("Place of birth of the person.")]
        public string? PlaceOfBirth { get; set; }

        [BsonElement("deathday")]
        [GraphQLName("deathday")]
        [GraphQLDescription("Death date of the person.")]
        public DateTime? DeathDate
        {
            get
            {
                return _deathDate;
            }
            set
            {
                DateTime dt;
                if (DateTime.TryParse(value.ToString(), out dt))
                {
                    if (dt.ToString() == "01-01-1970 00:00:00")
                    {
                        _deathDate = null;
                    }
                    else
                    {
                        _deathDate = Convert.ToDateTime(value);
                    }
                }
                else
                {
                    _deathDate = null;
                }
            }
        }

        [BsonElement("homepage")]
        [GraphQLName("homepage")]
        [GraphQLDescription("Website for the person.")]
        public string? Homepage { get; set; }

        [BsonElement("profile_path")]
        [GraphQLName("personimage")]
        [GraphQLDescription("Picture of the person.")]
        public string? Image { get; set; }

        [BsonElement("gender")]
        [GraphQLName("gender")]
        [GraphQLDescription("Gender of the person.")]
        public int Gender { get; set; }

        [BsonElement("known_for_department")]
        [GraphQLName("knownfordepartment")]
        [GraphQLDescription("Known for the department.")]
        public string KnownForDepartment { get; set; }

        [BsonElement("popularity")]
        [GraphQLName("popularity")]
        [GraphQLDescription("Popularity of the person.")]
        public double Popularity { get; set; }

        [BsonElement("external_links")]
        [GraphQLName("externallinks")]
        [GraphQLDescription("External/Social links of the person.")]
        public ExternalLinks? ExternalLinks { get; set; }
    }

    public class ExternalLinks
    {
        [BsonElement("freebase_mid")]
        [GraphQLName("freebasemid")]
        [GraphQLDescription("Freebase MID")]
        public string? FreebaseMId { get; set; }

        [BsonElement("freebase_id")]
        [GraphQLName("freebaseid")]
        [GraphQLDescription("Freebase ID")]
        public string? FreebaseId { get; set; }

        [BsonElement("imdb_id")]
        [GraphQLName("imdbid")]
        [GraphQLDescription("IMDB ID of the person")]
        public string? IMDBId { get; set; }

        [BsonElement("tvrage_id")]
        [GraphQLName("tvrageid")]
        [GraphQLDescription("TVRage ID of the person")]
        public string? TVRageId { get; set; }

        [BsonElement("facebook_id")]
        [GraphQLName("facebookid")]
        [GraphQLDescription("Facebook ID of the person")]
        public string? FacebookId { get; set; }

        [BsonElement("instagram_id")]
        [GraphQLName("instagramid")]
        [GraphQLDescription("Instagram ID of the person")]
        public string? InstagramId { get; set; }

        [BsonElement("twitter_id")]
        [GraphQLName("twitterid")]
        [GraphQLDescription("Twitter ID of the person")]
        public string? TwitterId { get; set; }

    }
}
