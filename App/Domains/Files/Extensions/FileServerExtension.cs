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

                    await FileProcessor.Write(file, eventContext.CancellationToken);
                }
            }
        });
}