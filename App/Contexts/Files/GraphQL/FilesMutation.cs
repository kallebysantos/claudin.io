using Microsoft.AspNetCore.WebUtilities;
using CloudIn.Domains.GraphQL;
using CloudIn.Domains.Data.Models;
using CloudIn.Contexts.Files.Repository;

namespace CloudIn.Contexts.Files.GraphQL;

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