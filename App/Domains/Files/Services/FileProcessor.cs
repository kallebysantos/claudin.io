using System.Text;
using tusdotnet.Interfaces;

namespace CloudIn.Domains.Files.Services;

public static class FileProcessor
{
    public async static Task<bool> WriteAsync(ITusFile file, CancellationToken cancellationToken)
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
            await fileContent.CopyToAsync(fileStream, cancellationToken);
        }

        await fileContent.DisposeAsync();
       
        return await Task.FromResult(true); 
    }

    public static async Task DeleteCacheAsync(ITusFile file, ITusTerminationStore terminationStore, CancellationToken cancellationToken)
    {
        await terminationStore.DeleteFileAsync(file.Id, cancellationToken);
    }
}