using HotChocolate.Data;
using MongoDB.Driver;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private string _connectionString = string.Empty;

        public MoviesRepository(IConfiguration configuration)
        {
            string? server = configuration["Server"];
            string? port = configuration["Port"];
            string? username = configuration["User"];
            string? password = configuration["Password"];
            string? database = configuration["Database"];

            if (string.IsNullOrEmpty(username))
            {
                _connectionString = $"mongodb://{server}:{port}";
            }
            else
            {
                _connectionString = $"mongodb://{username}:{password}@{server}:{port}";
            }

            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(database);
        }

        public async Task<IExecutable<Movie>> GetMovieById(object? MovieId)
        {
            FilterDefinition<Movie>? filter = null;

            if (MovieId != null)
            {
                // If MovieId is of type integer, then it is TMDB movie Id.
                // If MovieId is of type string, then it is IMDB Id.
                Int32 id32 = 0;
                Int64 id64 = 0;
                string id_s = string.Empty;
                string objType = MovieId.GetType().ToString();
                switch (objType)
                {
                    case "System.Int32":
                        Int32.TryParse(Convert.ToString(MovieId), out id32);
                        filter = Builders<Movie>.Filter.Eq(e => e.TMDBId, id32);
                        break;
                    case "System.Int64":
                        Int64.TryParse(Convert.ToString(MovieId), out id64);
                        filter = Builders<Movie>.Filter.Eq(e => e.TMDBId, id64);
                        break;
                    case "System.String":
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        id_s = Convert.ToString(MovieId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        filter = Builders<Movie>.Filter.Eq(e => e.IMDBId, id_s);
                        break;
                    default:
                        break;
                }
            }

            IMongoCollection<Movie> _collection = _database.GetCollection<Movie>("movies");

            return await Task.FromResult(_collection.Find(filter)
                .AsExecutable())
                .ConfigureAwait(false);
        }

        public async Task<IExecutable<Movie>> GetMovieById(int MovieId)
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

        public async Task<IExecutable<Credits>> GetMoviesByActor(string ActorName)
        {
            IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");

            var filter = Builders<Credits>
                .Filter
                .ElemMatch(
                e => e.Cast,
                e => e.KnownForDepartment == "Acting" &&
                e.Name.ToLowerInvariant() == ActorName.ToLowerInvariant());

            return await Task.FromResult(
                _collection.Find(filter)
                .AsExecutable())
                .ConfigureAwait(false);
        }

        public async Task<IExecutable<Keywords>> SearchMoviesByKeywords(string Keywords)
        {
            IMongoCollection<Keywords> _collection = _database.GetCollection<Keywords>("keywords");

            var filter = Builders<Keywords>
                .Filter
                .ElemMatch(
                e => e.Words,
                e => e.Name.ToLowerInvariant() == Keywords.ToLowerInvariant());

            return await Task.FromResult(
               _collection.Find(filter)
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

        public async Task<IExecutable<Keywords>> GetKeywords(int MovieId)
        {
            IMongoCollection<Keywords> _collection = _database.GetCollection<Keywords>("keywords");

            return await Task.FromResult(_collection.Find(c => c.Id == MovieId)
                .AsExecutable())
                .ConfigureAwait(false);
        }

        public async Task<IExecutable<Person>> SearchPerson(string PersonName = "")
        {
            IMongoCollection<Person> _collection = _database.GetCollection<Person>("person");

            if (string.IsNullOrEmpty(PersonName))
            {
                var f = await Task.FromResult(
                _collection
                .AsExecutable())
                .ConfigureAwait(false);

                return await Task.FromResult(
                _collection
                .AsExecutable())
                .ConfigureAwait(false);
            }

            return await Task.FromResult(
                _collection.Find(m => m.Name.ToLowerInvariant().Contains(PersonName.ToLowerInvariant()))
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
        // This approach is naive and much slower and query execution takes time.
        // The above implementation is recommended and is much faster.

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
