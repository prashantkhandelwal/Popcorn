using Popcorn.Models;
using Popcorn.Repositories;

namespace Popcorn.Queries.Extensions
{
        [ExtendObjectType(typeof(Credits))]
        public class CreditsMovieExtension
        {
            public async Task<IExecutable<Movie>> GetMovies([Parent] Credits credits, [Service] IMoviesRepository _movies)
            {
                return await _movies.GetMovieById(credits.Id).ConfigureAwait(false);
            }
        }
    }

