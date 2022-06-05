using tusdotnet;
using tusdotnet.Stores;
using tusdotnet.Models;
using tusdotnet.Interfaces;
using tusdotnet.Models.Configuration;
using CloudIn.Domains.Files.Services;

namespace CloudIn.Domains.Files.Extensions;

public static class FileServerExtension
{
    public static IApplicationBuilder UseFileHandler(this IApplicationBuilder app) =>
        app.UseTus(httpContext => new DefaultTusConfiguration
        {
            Store = new TusDiskStore(@"./Public/Cache"),
            UrlPath = "/files",
            Events = new Events
            {
                OnBeforeCreateAsync = async eventContext => 
                {
                    var value = eventContext.HttpContext.Request.Query["fileId"];

                    Console.WriteLine($"ID: {value}");

                    // Check if exists on database
                },
                OnFileCompleteAsync = async eventContext =>
                {
                    ITusFile file = await eventContext.GetFileAsync();
                    Console.WriteLine($"Upload of {eventContext.FileId} completed using {eventContext.Store.GetType().FullName}");

                    await FileProcessor.WriteAsync(file, eventContext.CancellationToken);

                    await FileProcessor.DeleteCacheAsync(
                        file: file,
                        terminationStore: (ITusTerminationStore)eventContext.Store,
                        cancellationToken: eventContext.CancellationToken
                    );
                }
            }
        });
}