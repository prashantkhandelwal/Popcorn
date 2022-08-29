using Popcorn.Models;

namespace Popcorn.Repositories
{
    public interface IMoviesRepository
    {
        Task<IExecutable<Movie>> GetMovieById(int IMDBId);

        Task<IExecutable<Movie>> GetMovieByName(string name);
    }
}
