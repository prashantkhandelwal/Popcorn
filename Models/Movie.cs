using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("movies")]
    public class Movie
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        public int TMDBId { get; set; }

        [BsonElement("imdb_id")]
        [GraphQLName("imdbid")]
        public string IMDBId { get; set; }

        [BsonElement("title")]
        [GraphQLName("title")]
        public string Title { get; set; }

        [BsonElement("original_title")]
        [GraphQLName("originaltitle")]
        public string OriginalTitle { get; set; }

        [BsonElement("original_language")]
        [GraphQLName("originallanguage")]
        public string OriginalLanguage { get; set; }

        [BsonElement("tagline")]
        [GraphQLName("tagline")]
        public string? TagLine { get; set; }

        [BsonElement("overview")]
        [GraphQLName("overview")]
        public string Overview { get; set; }

        [BsonElement("popularity")]
        [GraphQLName("popularity")]
        public double Popularity { get; set; }

        [BsonElement("production_companies")]
        [GraphQLName("productioncompanies")]
        public ProductionCompanies[]? ProductionCompanies { get; set; }

        [BsonElement("production_countries")]
        [GraphQLName("productioncountries")]
        public ProductionCountries[]? ProductionCountries { get; set; }

        [BsonElement("spoken_languages")]
        [GraphQLName("spokenlanguages")]
        public SpokenLanguages[] SpokenLanguages { get; set; }

        [BsonElement("adult")]
        [GraphQLName("isadult")]
        public bool IsAdult { get; set; }

        [BsonElement("homepage")]
        [GraphQLName("homepage")]
        public string Homepage { get; set; }

        [BsonElement("backdrop_path")]
        [GraphQLName("backdrop")]
        public string? Backdrop { get; set; }

        [BsonElement("poster_path")]
        [GraphQLName("poster")]
        public string? Poster { get; set; }

        [BsonElement("genres")]
        [GraphQLName("genres")]
        public Genres[]? Genres { get; set; }

        [BsonElement("runtime")]
        [GraphQLName("runtime")]
        public int Runtime { get; set; }

        [BsonElement("release_date")]
        [GraphQLName("releasedate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("budget")]
        [GraphQLName("budget")]
        public int Budget { get; set; }

        [BsonElement("revenue")]
        [GraphQLName("revenue")]
        public int Revenue { get; set; }

        [BsonElement("status")]
        [GraphQLName("status")]
        public string Status { get; set; }

        [BsonElement("video")]
        [GraphQLName("video")]
        public bool IsVideo { get; set; }

        [BsonElement("vote_average")]
        [GraphQLName("voteaverage")]
        public double VoteAverage { get; set; }

        [BsonElement("vote_count")]
        [GraphQLName("votecount")]
        public int VoteCount { get; set; }

    }

    public class Genres
    {
        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }
    }

    public class ProductionCompanies
    {
        [BsonElement("logo_path")]
        [GraphQLName("logo")]
        public string? LogoPath { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }

        [BsonElement("origin_country")]
        [GraphQLName("origincountry")]
        public string? OriginCountry { get; set; }
    }

    public class ProductionCountries
    {
        [BsonElement("iso_3166_1")]
        [GraphQLName("name")]
        public string CountryCode { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }
    }

    public class SpokenLanguages
    {
        [BsonElement("english_name")]
        [GraphQLName("language")]
        public string EnglishName { get; set; }

        [BsonElement("iso_639_1")]
        [GraphQLName("languagecode")]
        public string LanguageCode { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }
    }

    public class BelongsToCollection
    {
        [BsonElement("id")]
        [GraphQLName("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }

        [BsonElement("backdrop_path")]
        [GraphQLName("backdrop")]
        public string? Backdrop { get; set; }

        [BsonElement("poster_path")]
        [GraphQLName("poster")]
        public string? Poster { get; set; }
    }

}
