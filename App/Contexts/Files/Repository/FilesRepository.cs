using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;

namespace CloudIn.Contexts.Files.Repository;

public class FilesRepository : IFilesRepository
{
    private readonly DataContext _context;

    public FilesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddFileAsync(FileModel file)
    {
        await _context.Files.AddAsync(file);
    }

    public async Task<bool> SaveChangesAsync()
    {
        var hasSaved = await _context.SaveChangesAsync();

        return (hasSaved >= 0);
    }
}