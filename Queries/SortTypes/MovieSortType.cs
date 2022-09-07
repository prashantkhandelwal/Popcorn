using HotChocolate.Data.Sorting;
using Popcorn.Models;

namespace Popcorn.Queries.SortTypes
{
    public class MovieSortType: SortInputType<Movie>
    {
        protected override void Configure(ISortInputTypeDescriptor<Movie> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Popularity);
            descriptor.Field(f => f.Title);
            descriptor.Field(f => f.Revenue);
            descriptor.Field(f => f.ReleaseDate);
            descriptor.Field(f => f.VoteCount); 
            descriptor.Field(f => f.VoteAverage);
        }
    }
}
