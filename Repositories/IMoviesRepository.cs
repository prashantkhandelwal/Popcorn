using MongoDB.Driver;
using Popcorn.Models;

namespace Popcorn.Repositories
{
    public interface IMoviesRepository
    {
        Task<IExecutable<Movie>> SearchMovies();
    }
}
