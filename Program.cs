using Microsoft.EntityFrameworkCore;
using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Extensions;
using CloudIn.Domains.GraphQl;
using CloudIn.Contexts.Files;
using CloudIn.Contexts.Folders;
using CloudIn.Domains.Files.Extensions;
using CloudIn.Contexts.Files.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddDbContext<DataContext>(optBuilder =>
        optBuilder
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase("CloudInDatabase")
    )
    .AddGraphQLServer()
    .RegisterDbContext<DataContext>()
    .AddQueryType<RootQuery>()
    .AddMutationType<RootMutation>()
    .AddMutationConventions()
    .AddTypeExtension<FilesQuery>()
    .AddTypeExtension<FilesMutation>()
    .AddTypeExtension<FoldersQuery>();

builder.Services
    .AddScoped<IFilesRepository, FilesRepository>();


builder.Services
    .AddSpaStaticFiles(config =>
        config.RootPath = "Client/dist"
    );

var app = builder.Build();

app.UseFileHandler();

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

