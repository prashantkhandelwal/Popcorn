using HotChocolate.Data;
using Popcorn.Models;
using Popcorn.Queries.SortTypes;
using Popcorn.Repositories;

namespace Popcorn.Queries
{
    [GraphQLDescription("Query to search Movie Database.")]
    public class MoviesQuery
    {
        public MoviesQuery()
        {

        }

        // As UseProjection attribute is used here.
        // If you want to see how query is executed
        // in the database, then use the below commands
        // to log the queries in MongoDB:
        // db.setLogLevel(1)
        // db.setProfilingLevel(2)
        // db.system.profile.find().pretty()
        // db.adminCommand( { getLog:'global'} ).log.forEach(x => {print(x)})

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting(typeof(MovieSortType))]
        [GraphQLDescription("Search movies on the basis of properties defined in \"Movie\" schema.")]
        public async Task<IExecutable<Movie>> SearchMovies(IMoviesRepository _moviesRepository, string moviename = "")
        {
            return await _moviesRepository.SearchMovies(moviename).ConfigureAwait(false);
        }

        [UsePaging]
        [UseProjection]
        [GraphQLDescription("Get all movies by director name. The query will get \"Credits-Crew\" and then stich \"Movie\" schema to it.")]
        public async Task<IExecutable<Credits>> GetMoviesByDirector(string directorname, IMoviesRepository _moviesRepository)
        {
            return await _moviesRepository.GetMoviesDirectedBy(directorname);
        }

        [UsePaging]
        [UseProjection]
        [GraphQLDescription("Get all movies by actor name. The query will get \"Credits-Cast\" and then stich \"Movie\" schema to it.")]
        public async Task<IExecutable<Credits>> GetMoviesByActor(string actorname, IMoviesRepository _moviesRepository)
        {
            return await _moviesRepository.GetMoviesByActor(actorname);
        }

        [UsePaging]
        [UseProjection]
        [GraphQLDescription("Get all movies by keywords. The query will search \"Keywords\" and then stich \"Movies\" schema to it.")]
        public async Task<IExecutable<Keywords>> GetMoviesByKeywords(string keywords, IMoviesRepository _moviesRepository)
        {
            return await _moviesRepository.SearchMoviesByKeywords(keywords);
        }

        [UsePaging]
        [UseProjection]
        [GraphQLDescription("Get a movie by the providing the TMDBID (integer) e.g. 2, 857 etc. or IMDBID (string) e.g. tt0116629.")]
        public async Task<IExecutable<Movie>> GetMovieById([GraphQLType(typeof(AnyType))] object? movieid, IMoviesRepository _moviesRepository)
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
