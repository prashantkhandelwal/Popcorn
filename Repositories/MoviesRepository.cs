using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        //private IMongoCollection<BsonDocument>? _collection;

        public MoviesRepository()
        {
            _client = new MongoClient("mongodb://127.0.0.1");
            _database = _client.GetDatabase("moviedb");
        }

        public async Task<IExecutable<Credits>> GetCredits(int MovieId)
        {
            IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");
            return await Task.FromResult(_collection.Find(c => c.Id == MovieId)
                .AsExecutable())
                .ConfigureAwait(false);
        }

        public async Task<IExecutable<Movie>> GetMoviesById(int MovieId)
        {
            IMongoCollection<Movie> _collection = _database.GetCollection<Movie>("movies");
            return await Task.FromResult(_collection.Find(c => c.TMDBId == MovieId)
                .AsExecutable())
                .ConfigureAwait(false);
        }


        public async Task<IExecutable<Movie>> SearchMovies(string MovieName = "")
        {
            IMongoCollection<Movie> _collection = _database.GetCollection<Movie>("movies");

            if (string.IsNullOrEmpty(MovieName))
            {
                return _collection.AsExecutable();
            }

            return await Task.FromResult(
                _collection.Find(m => m.Title.ToLowerInvariant().Contains(MovieName.ToLowerInvariant()))
                .AsExecutable())
                .ConfigureAwait(false);
        }


        public async Task<IExecutable<Credits>> GetMoviesDirectedBy(string DirectorName)
        {
            IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");
            
            var filter = Builders<Credits>
                .Filter
                .ElemMatch(
                e => e.Crew,
                e => e.Job == "Director" &&
                e.Name.ToLowerInvariant() == DirectorName.ToLowerInvariant());

            return await Task.FromResult(
                _collection.Find(filter)
                .AsExecutable())
                .ConfigureAwait(false);
        }



        #region Other Implementations

        // Below code can also be used to get the movies by director name
        // This approach is naive and much slower and quesry execution takes time.
        // The above implementation is recommended and is much faster coparatively.

        //public async Task<List<Movie>> GetMoviesDirectedBy(string DirectorName)
        //{
        //    IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");

        //    var filter = Builders<Credits>
        //        .Filter
        //        .ElemMatch(
        //        e => e.Crew,
        //        e => e.Job == "Director" &&
        //        e.Name.ToLowerInvariant() == DirectorName.ToLowerInvariant());

        //    var result = await _collection.Aggregate(options: new AggregateOptions { Comment = "DirectorMovies"})
        //        .Match(filter)
        //        .Lookup("movies", "id", "id", "movies")
        //        .Project("{'movies': 1, '_id': 0}")
        //        .ToListAsync()
        //        .ConfigureAwait(false);

        //    List<Movie> allMovies = new List<Movie>();
        //    Movie movie = new Movie();

        //    foreach (var item in result)
        //    {
        //        movie = BsonSerializer.Deserialize<Movie>(item[0][0].AsBsonDocument);
        //        allMovies.Add(movie);
        //    }
        //    return allMovies;
        //}

        #endregion
    }
}
