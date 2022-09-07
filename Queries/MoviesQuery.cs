using HotChocolate.Data;
using Popcorn.Models;
using Popcorn.Queries.SortTypes;
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
        // db.adminCommand( { getLog:'global'} ).log.forEach(x => {print(x)})

        [UsePaging(MaxPageSize = 50)]
        [UseProjection]
        [UseFiltering]
        [UseSorting(typeof(MovieSortType))]
        public async Task<IExecutable<Movie>> SearchMovies(string moviename="")
        {
            return await _moviesRepository.SearchMovies(moviename).ConfigureAwait(false);
        }

        [UseProjection]
        public async Task<IExecutable<Credits>> GetMoviesByDirector(string directorname)
        {
            return await _moviesRepository.GetMoviesDirectedBy(directorname);
        }

        //TODO: Remove after testing.
        //[UseProjection]
        //[UseFiltering]
        //public async Task<IExecutable<Movie>> SearchMovies()
        //{
        //    return await _moviesRepository.SearchMovies("");
        //}
    }
}
