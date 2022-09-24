using Popcorn.Models;
using Popcorn.Repositories;

namespace Popcorn.Queries.Extensions
{
    [ExtendObjectType(typeof(Movie))]
    public class MovieKeywordsExtension
    {
        public async Task<IExecutable<Keywords>> GetKeywords([Parent] Movie movie, [Service] IMoviesRepository _movies)
        {
            return await _movies.GetKeywords(movie.TMDBId).ConfigureAwait(false);
        }
    }
}
