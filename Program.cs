using Popcorn.Repositories;
using Popcorn.Queries;
using HotChocolate.AspNetCore.Voyager;
using Popcorn.Queries.Extensions;
using Microsoft.Extensions.FileProviders;
using Path = System.IO.Path;
using HotChocolate.Execution.Options;
using HotChocolate.Types.Pagination;
using Prometheus;
using Popcorn.Monitoring;

var builder = WebApplication.CreateBuilder(args);

string env = (builder.Environment.IsDevelopment()) ? string.Empty : ".Production";
builder.Configuration.AddJsonFile($"appsettings{env}.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

PagingOptions pagingOptions = new PagingOptions
{
    DefaultPageSize = builder.Configuration.GetValue<int>("PageSize"),
    MaxPageSize = builder.Configuration.GetValue<int>("MaxPageSize"),
    IncludeTotalCount = builder.Configuration.GetValue<bool>("IncludePageTotalCount"),
    AllowBackwardPagination = builder.Configuration.GetValue<bool>("AllowBackwardPagination"),
};

bool enableMonitoring = builder.Configuration.GetValue<bool>("Monitoring");

builder.Services.AddRouting();
builder.Services.AddTransient<IMoviesRepository, MoviesRepository>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MoviesQuery>()
    .SetPagingOptions(pagingOptions)
    .RegisterService<IMoviesRepository>()
    .AddTypeExtension<MovieCreditsExtension>()
    .AddTypeExtension<KeywordsMovieExtension>()
    .AddTypeExtension<CreditsMovieExtension>()
    .AddMongoDbPagingProviders()
    .AddMongoDbProjections()
    .AddMongoDbFiltering()
    .AddMongoDbSorting()
    .SetRequestOptions(_ => new RequestExecutorOptions { ExecutionTimeout = TimeSpan.FromMinutes(1) });

builder.Services.AddHealthChecks()
    .AddCheck<PopcornHealthCheck>(nameof(PopcornHealthCheck))
    .ForwardToPrometheus();

var app = builder.Build();

app.UseRouting();

// Enable Prometheus Monitoring
if (enableMonitoring)
{
    app.UseMetricServer();
    app.UseHttpMetrics();
}

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
