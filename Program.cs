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
using System.Text;

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
            Console.WriteLine($"Upload of {eventContext.FileId} completed using {eventContext.Store.GetType().FullName}");

            if (file == null)
            {
                return;
            }

            var result = await DoSomeProcessing(file, eventContext.CancellationToken).ConfigureAwait(false);
            if (!result)
            {
                //throw new MyProcessingException("Something went wrong during processing");
            }
        }
    }
});

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

async Task<bool> DoSomeProcessing(ITusFile file, CancellationToken cancellationToken)
{
    var metadata = await file.GetMetadataAsync(cancellationToken);

    var fileType = metadata["filetype"].GetString(Encoding.UTF8);

    var fileExtension = "";

    if (fileType == "image/png")
    {
        fileExtension = ".png";
    }

    var fileContent = await file.GetContentAsync(cancellationToken);

    var fileName = $"file{fileExtension}";
    var folderPath = $"./Public/{file.Id}";

    Directory.CreateDirectory(folderPath);

    using (var fileStream = new FileStream($"{folderPath}/{fileName}", FileMode.CreateNew, FileAccess.Write))
    {
        await fileContent.CopyToAsync(fileStream);
    }

    return await Task.FromResult(true);
}