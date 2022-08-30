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

        public async Task<IExecutable<Movie>> SearchMovies()
        {
            return await Task.FromResult(_collection.AsExecutable()).ConfigureAwait(false);
        }
    }
}
