using MongoDB.Bson;
using MongoDB.Driver;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public interface IMoviesRepository
    {
        Task<IExecutable<Movie>> SearchMovies(string MovieName);

        Task<IExecutable<Credits>> GetCredits(int MovieId);

        Task<IExecutable<Movie>> GetMoviesById(int MovieId);

        Task<IExecutable<Credits>> GetMoviesDirectedBy(string DirectorName);

        //Task<IExecutable<BsonDocument>> GetMoviesByDirector(string DirectorName);

    }
}
