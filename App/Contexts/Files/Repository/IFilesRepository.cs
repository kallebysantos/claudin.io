using CloudIn.Domains.Data.Models;
using CloudIn.Contexts.Files.Contracts.Inputs;

namespace CloudIn.Contexts.Files.Repository;

public interface IFilesRepository
{
    Task<FileModel>CreateFileAsync(CreateFileInput fileInput);

    Task AddFileAsync(FileModel file);

    Task<bool> SaveChangesAsync();
}