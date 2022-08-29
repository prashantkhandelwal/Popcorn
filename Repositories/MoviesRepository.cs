using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Movie> _collection;

        public MoviesRepository()
        {
            _client = new MongoClient("mongodb://127.0.0.1");
            _database = _client.GetDatabase("moviedb");
            _collection = _database.GetCollection<Movie>("movies");
        }

        public async Task<IExecutable<Movie>> GetMovieById(int TMDBId)
        {
            return await Task.FromResult(_collection.Find(m => m.TMDBId == TMDBId).AsExecutable()).ConfigureAwait(false);
        }

        public async Task<IExecutable<Movie>> GetMovieByName(string Name)
        {
            //var builder = Builders<Movie>.Filter;
            //var filter = builder.Eq("original_title", Name) & builder.Eq("title", Name);
            // https://stackoverflow.com/questions/33058061/c-sharp-mongodb-case-sensitive-search

            return await Task.FromResult(_collection
                .Find(m => m.OriginalTitle.ToLowerInvariant() == Name.ToLowerInvariant() || m.Title.ToLowerInvariant() == Name.ToLowerInvariant())
                .AsExecutable()).ConfigureAwait(false);
        }
    }
}
