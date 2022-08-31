using HotChocolate.Data;
using MongoDB.Bson;
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

        public async Task<IExecutable<Movie>> SearchMovies(string MovieName="")
        {
            IMongoCollection<Movie> _collection = _database.GetCollection<Movie>("movies");

            if(string.IsNullOrEmpty(MovieName))
            {
                return _collection.AsExecutable();
            }

            return await Task.FromResult(
                _collection.Find(m => m.Title.ToLowerInvariant().Contains(MovieName.ToLowerInvariant()))
                .AsExecutable())
                .ConfigureAwait(false);
        }

        //public async Task<IExecutable<Movie>> GetMoviesByDirector(string DirectorName)
        //{
        //    IMongoCollection<Credits> _collection2 = _database.GetCollection<Credits>("credits");

        //    var filter = Builders<Credits>
        //        .Filter
        //        .ElemMatch(
        //        e => e.Crew,
        //        e => e.Job == "Director" &&
        //        e.Name.ToLowerInvariant() == DirectorName.ToLowerInvariant());

        //    return await Task.FromResult(_collection2.Aggregate()
        //        .Match(filter)
        //        .Lookup("movies", "id", "id", "movies")
        //        .Project("{'allmovies.title': 1, '_id': 0}")
        //        .AsExecutable()).ConfigureAwait(false);
        //}

            //    IMongoCollection<Credits> _collection = _database.GetCollection<Credits>("credits");
            //    //IMongoCollection<Movie> _collection2 = _database.GetCollection<Movie>("movies");

            //    //var filter = Builders<Credits>.Filter.Eq(x => x.Crew.Where(c => c.Name == DirectorName.ToLowerInvariant()).Count(), 0);

            //    BsonDocument pipelineStage1 = new BsonDocument
            //    {
            //        {
            //            "$match", new BsonDocument
            //            {
            //                {"crew.name", DirectorName }
            //            }
            //        }
            //    };

            //    BsonDocument[] pipeline = new BsonDocument[] { pipelineStage1 };

            //    List<BsonDocument> results = _collection.Aggregate<BsonDocument>(pipeline).ToList();


            //    return await Task.FromResult(results);

            //return await Task.FromResult(_collection.Aggregate()
            //    .Match(filter)
            //    .Lookup<Credits, Movie, Movie>(
            //        _collection2,
            //        localField => localField.Id,
            //        foreignField => foreignField.TMDBId,
            //        output => output.Title).AsExecutable()).ConfigureAwait(false);



            /*//var filter = Builders<Credits>
            //    .Filter
            //    .ElemMatch(
            //    e => e.Crew,
            //    e => e.Job == "Director" &&
            //    e.Name.ToLowerInvariant() == DirectorName.ToLowerInvariant());

            var result = _collection.Aggregate()
                .Lookup(foreignCollectionName: "movies",
                localField: "id",
                foreignField: "id",
                @as: "allmovies")
                .Unwind("allmovies")
                .Match(new BsonDocument()
                {
                    {
                        "$expr", new BsonDocument()
                        {
                            {
                                "$and", new BsonArray()
                                {
                                    new BsonDocument() {{ "$eq", new BsonArray(){"$crew.name", DirectorName.ToLowerInvariant() } } },
                                    new BsonDocument() {{ "$eq", new BsonArray(){"$crew.job", "director" }},
                                } 
                                }
                            }
                        }
                    }

                });

            return await Task.FromResult(result.AsExecutable()).ConfigureAwait(false);*/
            //}
            //}
        }
}
