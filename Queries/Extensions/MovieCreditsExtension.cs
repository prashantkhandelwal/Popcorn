using MongoDB.Bson.Serialization.Attributes;
using Popcorn.Models;
using Popcorn.Repositories;

namespace Popcorn.Queries.Extensions
{
    [ExtendObjectType(typeof(Movie))]
    public class MovieCreditsExtension
    {
        public async Task<IExecutable<Credits>> GetCredits([Parent]Movie movie, [Service] IMoviesRepository _movies)
        {
            return await _movies.GetCredits(movie.TMDBId).ConfigureAwait(false);
        }   
    }
}
