using CloudIn.Domains.Data.Models;

namespace CloudIn.Contexts.Files.Repository;

public interface IFilesRepository
{
    Task AddFileAsync(FileModel file);

    Task<bool> SaveChangesAsync();
}