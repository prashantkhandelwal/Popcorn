using Popcorn.Models;

namespace Popcorn.Repositories
{
    public interface IMoviesRepository
    {
        Task<IExecutable<Movie>> GetMovieById(string IMDBId);

        Task<IExecutable<Movie>> GetMovieByName(string name);
    }
}
