using Microsoft.AspNetCore.WebUtilities;
using CloudIn.Domains.GraphQL;
using CloudIn.Domains.Data.Models;
using CloudIn.Contexts.Files.Repository;
using CloudIn.Contexts.Files.Contracts.Inputs;

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
        CreateFileInput createFileInput
    )
    {
        var baseUrl = $"{httpContextAccessor?.HttpContext?.Request.Host.ToUriComponent()}/files";

        var file = await _filesRepository.CreateFileAsync(createFileInput);

        await _filesRepository.AddFileAsync(file);

        await _filesRepository.SaveChangesAsync();
        
        var uploadUrl = QueryHelpers.AddQueryString(baseUrl, "fileId", file.Id.ToString());

        return new(uploadUrl);
    }
}