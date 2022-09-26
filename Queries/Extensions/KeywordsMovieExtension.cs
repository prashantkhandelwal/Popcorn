using Popcorn.Models;
using Popcorn.Repositories;

namespace Popcorn.Queries.Extensions
{
    [ExtendObjectType(typeof(Keywords))]
    public class KeywordsMovieExtension
    {
        public async Task<IExecutable<Movie>> GetMovies([Parent] Keywords key, [Service] IMoviesRepository _movies)
        {
            return await _movies.GetMovieById(key.Id).ConfigureAwait(false);
        }
    }
}
