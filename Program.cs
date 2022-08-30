using Popcorn.Repositories;
using Popcorn.Queries;
using HotChocolate.AspNetCore.Voyager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MoviesQuery>()
    .AddMongoDbProjections()
    .AddMongoDbFiltering();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

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
