using Popcorn.Repositories;
using Popcorn.Queries;
using HotChocolate.AspNetCore.Voyager;
using Popcorn.Queries.Extensions;
using Microsoft.Extensions.FileProviders;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MoviesQuery>()
    .AddTypeExtension<MovieCreditsExtension>()
    .AddTypeExtension<KeywordsMovieExtension>()
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

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Web")),
    RequestPath = "/docs"
});

app.MapGraphQL();

app.UseVoyager(new VoyagerOptions
{
    Path = "/voyager",
    QueryPath = "/graphql"
});

app.Run();
