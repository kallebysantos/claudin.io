using CloudIn.Contexts.Files.Repository;
using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQl;
using Microsoft.AspNetCore.WebUtilities;

namespace CloudIn.Contexts.Files;

[ExtendObjectType(typeof(RootMutation))]
public class FilesMutation
{
    private readonly IFilesRepository _filesRepository;

    public FilesMutation(IFilesRepository filesRepository)
    {
        _filesRepository = filesRepository;
    }

    public async Task<string> UploadFileAsync(
        [Service] IHttpContextAccessor httpContextAccessor,
        string name
    )
    {
        var baseUrl = $"{httpContextAccessor?.HttpContext.Request.Host.ToUriComponent()}/files";

        var fileObject = new FileModel
        {
            Name = name,
        };

        await _filesRepository.AddFileAsync(fileObject);

        await _filesRepository.SaveChangesAsync();
        
        var uploadUrl = QueryHelpers.AddQueryString(baseUrl, "fileId", fileObject.Id.ToString());

        return new(uploadUrl);
    }
}