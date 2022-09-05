using HotChocolate.Data.Sorting;
using Popcorn.Models;

namespace Popcorn.Queries.SortTypes
{
    public class PopularitySortType: SortInputType<Movie>
    {
        protected override void Configure(ISortInputTypeDescriptor<Movie> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Popularity);
        }
    }
}
