using MongoDB.Bson.Serialization.Attributes;

namespace Popcorn.Models
{
    [BsonDiscriminator("imdbmovies")]
    public class IMDBMovie
    {
        private bool _isAdult;

        public object _id { get; set; }

        [BsonElement("tconst")]
        public string IMDBId { get; set; }

        [BsonElement("primaryTitle")]
        public string PrimaryTitle { get; set; }

        [BsonElement("originalTitle")]
        public string OriginalTitle { get; set; }

        [BsonElement("titleType")]
        public string TitleType { get; set; }

        [BsonElement("isAdult")]
        [GraphQLType(typeof(AnyType))]
        public object IsAdult
        {
            get
            {
                return _isAdult;
            }

            set
            {
                if (value.GetType() == typeof(string))
                {
                    if (value.ToString() == "0")
                        _isAdult = true;

                    _isAdult = false;
                }
            }
        }

        [BsonElement("startYear")]
        public int StartYear { get; set; }


        // In the data imported, endYear can be
        // '\n'

        [BsonElement("endYear")]
        public string EndYear { get; set; }

        [BsonElement("runtimeMinutes")]
        public string RuntimeMinutes { get; set; }

        [BsonElement("genres")]
        public string Genres { get; set; }
    }
}
