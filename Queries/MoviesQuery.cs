using HotChocolate.Data;
using Popcorn.Models;
using Popcorn.Repositories;

namespace Popcorn.Queries
{
    public class MoviesQuery
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesQuery(IMoviesRepository repo)
        {
            _moviesRepository = repo;
        }

        // As UseProjection attribute is used here.
        // If you want to see how query is executed
        // in the database, then use the below commands
        // to log the queries in MongoDB:
        // db.setLogLevel(1)
        // db.setProfilingLevel(2)
        // db.system.profile.find().pretty()

        [UseProjection]
        public async Task<IExecutable<Movie>> GetMovie(int id)
        {
            var movie = await _moviesRepository.GetMovieById(id).ConfigureAwait(false);
            return movie;
        }

        [UseProjection]
        public async Task<IExecutable<Movie>> GetMovieByName(string name)
        {
            var movie = await _moviesRepository.GetMovieByName(name).ConfigureAwait(false);
            return movie;
        }
    }
}
