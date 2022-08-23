using HotChocolate.Data;
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
            _database = _client.GetDatabase("imdb");
            _collection = _database.GetCollection<Movie>("movies");
        }

        public async Task<IExecutable<Movie>> GetMovieById(string IMDBId)
        {
            return await Task.FromResult(_collection.Find(m => m.IMDBId.ToLowerInvariant() == IMDBId.ToLowerInvariant()).AsExecutable()).ConfigureAwait(false);
        }

        public async Task<IExecutable<Movie>> GetMovieByName(string Name)
        {
            var builder = Builders<Movie>.Filter;
            var filter = builder.Eq("originalTitle", Name) & builder.Eq("titleType", "movie");

            return await Task.FromResult(_collection.Find(filter).AsExecutable()).ConfigureAwait(false);
        }
    }
}
