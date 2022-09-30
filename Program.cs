using Popcorn.Repositories;
using Popcorn.Queries;
using HotChocolate.AspNetCore.Voyager;
using Popcorn.Queries.Extensions;
using Microsoft.Extensions.FileProviders;
using Path = System.IO.Path;
using HotChocolate.Execution.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MoviesQuery>()
    .RegisterService<IMoviesRepository>()
    .AddTypeExtension<MovieCreditsExtension>()
    .AddTypeExtension<KeywordsMovieExtension>()
    .AddTypeExtension<CreditsMovieExtension>()
    .AddMongoDbPagingProviders()
    .AddMongoDbProjections()
    .AddMongoDbFiltering()
    .AddMongoDbSorting()
    .SetRequestOptions(_ => new RequestExecutorOptions { ExecutionTimeout = TimeSpan.FromMinutes(1) });

var app = builder.Build();

string env = (app.Environment.IsDevelopment()) ? string.Empty : ".Production";
builder.Configuration.AddJsonFile($"appsettings{env}.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", e =>
    {
        e.Response.Redirect("/docs/index.html", permanent: false);
        return Task.CompletedTask;
    });
});

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
