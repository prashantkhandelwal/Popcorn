using HotChocolate.Data;
using Popcorn.Models;
using Popcorn.Queries.SortTypes;
using Popcorn.Repositories;

namespace Popcorn.Queries
{
    [GraphQLDescription("Query to search Movie Database.")]
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
        [GraphQLDescription("Search movies on the basis of properties defined in \"Movie\" schema.")]
        public async Task<IExecutable<Movie>> SearchMovies(string moviename="")
        {
            return await _moviesRepository.SearchMovies(moviename).ConfigureAwait(false);
        }

        [UsePaging(MaxPageSize = 50)]
        [UseProjection]
        [GraphQLDescription("Get all movies by director name. The query will get \"Credits-Crew\" and then strich \"Movie\" schema to it.")]
        public async Task<IExecutable<Credits>> GetMoviesByDirector(string directorname)
        {
            return await _moviesRepository.GetMoviesDirectedBy(directorname);
        }

        [UseProjection]
        [GraphQLDescription("Get a movie by the providing the TMDBID (integer) e.g. 2, 857 etc. or IMDBID (string) e.g. tt0116629.")]
        public async Task<IExecutable<Movie>> GetMovieById([GraphQLType(typeof(AnyType))] object? movieid)
        {
            return await _moviesRepository.GetMovieById(movieid).ConfigureAwait(false);
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
