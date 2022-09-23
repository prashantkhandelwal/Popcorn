using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{

    [BsonIgnoreExtraElements]
    [BsonDiscriminator("movies")]
    [GraphQLDescription("All details of the movie.")]
    public class Movie
    {
        [GraphQLType(typeof(Guid))]
        [BsonId]
        public object _id { get; set; }

        [BsonElement("id")]
        [GraphQLName("id")]
        [GraphQLDescription("TMDB Movie Id. This is the unique ID for movies listed in TheMovieDB.org. The ID is in integer format.")]
        public int TMDBId { get; set; }

        [BsonElement("imdb_id")]
        [GraphQLName("imdbid")]
        [GraphQLDescription("IMDB Movie Id. This is the unique ID for movies listed in IMDB.com. The ID is in string format. E.g.: tt0076759")]
        public string IMDBId { get; set; }

        [BsonElement("title")]
        [GraphQLName("title")]
        [GraphQLDescription("TMDB Movie Title.")]
        public string Title { get; set; }

        [BsonElement("belongs_to_collection")]
        [GraphQLName("belongstocollection")]
        [GraphQLDescription("Collection name which this movie belongs to. Refer to \"BelongsToCollection\" schema.")]
        public BelongsToCollection? BelongsToCollection { get; set; }

        [BsonElement("original_title")]
        [GraphQLName("originaltitle")]
        [GraphQLDescription("TMDB Original Movie Title. Also refer to \"AlternativeTitles\" schema.")]
        public string OriginalTitle { get; set; }

        [BsonElement("original_language")]
        [GraphQLName("originallanguage")]
        [GraphQLDescription(
            "TMDB Original Language for the movie. " +
            "Also refer to \"ReleaseDates\" schema to view movie release date in other countries.")]
        public string OriginalLanguage { get; set; }

        [BsonElement("tagline")]
        [GraphQLName("tagline")]
        [GraphQLDescription("TagLine of the movie.")]
        public string? TagLine { get; set; }

        [BsonElement("overview")]
        [GraphQLName("overview")]
        [GraphQLDescription("Entire \"Overview\" of the movie. Consider this as a summary for the movie.")]
        public string Overview { get; set; }

        [BsonElement("popularity")]
        [GraphQLName("popularity")]
        [GraphQLDescription("Popularity of the movie. This metric is specific to TMDB.")]
        public double Popularity { get; set; }

        [BsonElement("production_companies")]
        [GraphQLName("productioncompanies")]
        [GraphQLDescription("All \"ProductionCompanies\" involves in the production of the movie." +
            "You can query: logo, name and origincountry fields.")]
        public ProductionCompanies[]? ProductionCompanies { get; set; }

        [BsonElement("production_countries")]
        [GraphQLName("productioncountries")]
        [GraphQLDescription("All \"ProductionCountries\" involves in the production of the movie." +
            "You can query: countrycode and countryname fields.")]
        public ProductionCountries[]? ProductionCountries { get; set; }

        [BsonElement("spoken_languages")]
        [GraphQLName("spokenlanguages")]
        public SpokenLanguages[] SpokenLanguages { get; set; }

        [BsonElement("adult")]
        [GraphQLName("isadult")]
        [GraphQLDescription("If movie is an adult movie.")]
        public bool IsAdult { get; set; }

        [BsonElement("homepage")]
        [GraphQLName("homepage")]
        [GraphQLDescription("Website for the movie.")]
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
        public long Revenue { get; set; }

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

    [BsonIgnoreExtraElements]
    [GraphQLDescription("All Genres of the movie.")]
    public class Genres
    {
        [BsonElement("name")]
        [GraphQLName("name")]
        public string Name { get; set; }
    }

    [BsonIgnoreExtraElements]
    [GraphQLDescription("All Production Companies involved in the production of the movie.")]
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

    [BsonIgnoreExtraElements]
    [GraphQLDescription("All Production Countries of the movie.")]
    public class ProductionCountries
    {
        [BsonElement("iso_3166_1")]
        [GraphQLName("countrycode")]
        public string CountryCode { get; set; }

        [BsonElement("name")]
        [GraphQLName("countryname")]
        public string Name { get; set; }
    }

    [BsonIgnoreExtraElements]
    [GraphQLDescription("All Spoken Languages movie was released in.")]
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

    [BsonIgnoreExtraElements]
    [GraphQLDescription("Collection names which movie belongs to.")]
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


    //public class MovieTypeExtension : ObjectTypeExtension
    //{
    //    protected override void Configure(IObjectTypeDescriptor descriptor)
    //    {
    //        descriptor.Name("Movies");

    //        descriptor.Field("GetCredits")
    //            .ResolveWith<CreditsResolver>(_ => _.GetCredits())
    //            .Type<StringType>()
    //            .Name("Credits");

    //    }
    //}

}
