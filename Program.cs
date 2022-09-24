using Popcorn.Repositories;
using Popcorn.Queries;
using HotChocolate.AspNetCore.Voyager;
using Popcorn.Queries.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MoviesQuery>()
    .AddTypeExtension<MovieCreditsExtension>()
    .AddTypeExtension<MovieKeywordsExtension>()
    .AddTypeExtension<CreditsMovieExtension>()
    .AddMongoDbPagingProviders()
    .AddMongoDbProjections()
    .AddMongoDbFiltering()
    .AddMongoDbSorting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Do something when debugging.
}

app.UseRouting();

app.MapGraphQL();

app.UseVoyager(new VoyagerOptions
{
    Path = "/voyager",
    QueryPath = "/graphql"
});


app.Run();
