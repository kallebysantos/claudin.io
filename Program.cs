using Microsoft.EntityFrameworkCore;
using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Extensions;
using CloudIn.Domains.GraphQl;
using CloudIn.Contexts.Files;
using CloudIn.Contexts.Folders;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Stores;
using tusdotnet.Models.Configuration;
using Microsoft.AspNetCore.Http.Features;
using tusdotnet.Interfaces;

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
    .AddTypeExtension<FilesQuery>()
    .AddTypeExtension<FoldersQuery>();


builder.Services
    .AddSpaStaticFiles(config =>
        config.RootPath = "Client/dist"
    );

var app = builder.Build();

app.Use((context, next) =>
{
    // Default limit was changed some time ago. Should work by setting MaxRequestBodySize to null using ConfigureKestrel but this does not seem to work for IISExpress.
    // Source: https://github.com/aspnet/Announcements/issues/267
    context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;
    return next.Invoke();
});

app.UseTus(httpContext => new DefaultTusConfiguration
{
    Store = new TusDiskStore(@"./Public/Uploads"),
    UrlPath = "/files",
    Events = new Events
    {
        OnFileCompleteAsync = async eventContext =>
        {
            // eventContext.FileId is the id of the file that was uploaded.
            // eventContext.Store is the data store that was used (in this case an instance of the TusDiskStore)

            // A normal use case here would be to read the file and do some processing on it.
            ITusFile file = await eventContext.GetFileAsync();
            Console.WriteLine(file.Id);

            var result = await DoSomeProcessing(file, eventContext.CancellationToken).ConfigureAwait(false);

            if (!result)
            {
                //throw new MyProcessingException("Something went wrong during processing");
            }
        }
    }
});

app.UseRouting();

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

Task<bool> DoSomeProcessing(ITusFile file, CancellationToken cancellationToken)
{
    return Task.FromResult(true);
}