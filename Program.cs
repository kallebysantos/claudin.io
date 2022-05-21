using Microsoft.EntityFrameworkCore;
using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Extensions;
using CloudIn.Domains.GraphQl;
using CloudIn.Contexts.Files;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<DataContext>(optBuilder =>
        optBuilder
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase("CloudInDatabase")
    )
    .AddGraphQLServer()
    .RegisterDbContext<DataContext>()
    .AddQueryType<RootQuery>()
    .AddTypeExtension<FilesQuery>();

builder.Services
    .AddSpaStaticFiles(config =>
        config.RootPath = "Client/dist"
    );

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(config =>
{
    config.MapGraphQL("/api/graphql");
});

app.UseSpaStaticFiles();
app.UseSpa(spaBuilder =>
{
    spaBuilder.Options.SourcePath = "Client";

    if (builder.Environment.IsDevelopment())
    {
        spaBuilder.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

if (builder.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.Run();
