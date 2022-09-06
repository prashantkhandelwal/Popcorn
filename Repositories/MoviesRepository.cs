using HotChocolate.Data;
using MongoDB.Driver;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        public MoviesRepository()
        {
            _client = new MongoClient("mongodb://127.0.0.1");
            _database = _client.GetDatabase("moviedb");
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
                return await Task.FromResult(
                _collection
                .AsExecutable())
                .ConfigureAwait(false);
            }

            return await Task.FromResult(
                _collection.Find(m => m.Title.ToLowerInvariant().Contains(MovieName.ToLowerInvariant()))
                .AsExecutable())
                .ConfigureAwait(false);
        }

        public async Task<IExecutable<Credits>> GetCredits(int MovieId = 0)
        {
            IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");
            if (MovieId == 0)
            {
                return await Task.FromResult(_collection
                    .AsExecutable())
                    .ConfigureAwait(false);
            }

            return await Task.FromResult(_collection.Find(c => c.Id == MovieId)
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


        // Below code can be used to get the movies based on the name passed as an argument
        // Commented this out as the HotChocolate filter function is taking care of filtering.
        // HotChocolate search is case-sensitive.
        //
        // If you use the below code, then the GraphQL query will be
        // query ($name: [String!]!) {
        //    moviesByGenre(genrename: $name) {
        //    id
        //    title
        //   }
        // }
        //
        //
        // Variables:
        //  {
        //    "name": ["Crime", "Adventure"]
        //  }
        public async Task<IExecutable<Movie>> GetMoviesByGenre(string[] GenreNames)
        {
            IMongoCollection<Movie> _collection = _database.GetCollection<Movie>("movies");



            //var filter = Builders<Movie>
            //    .Filter
            //    .ElemMatch(
            //     e => e.Genres,
            //     e => e.Name.ToLowerInvariant() == GenreName.ToLowerInvariant());

            var filter = Builders<Genres>
                        .Filter
                        .In(
                         e => e.Name, GenreNames);

            var moviesbygenre = Builders<Movie>.Filter.ElemMatch(t => t.Genres, filter);

            return await Task.FromResult(
                _collection.Find(moviesbygenre)
                .AsExecutable())
                .ConfigureAwait(false);
        }

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
