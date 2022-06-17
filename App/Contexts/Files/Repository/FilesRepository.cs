using CloudIn.Contexts.Files.Contracts.Inputs;
using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<FileModel> CreateFileAsync(CreateFileInput fileInput)
    {
        var parentFolder = await _context.Folders
            .FirstOrDefaultAsync(folder => folder.Id == fileInput.ParentFolderId);

        if(parentFolder == null) throw new NullReferenceException("Parent folder not found");

        var file = new FileModel
        {
            Id = new Guid(),
            Name = fileInput.Name
        };

        parentFolder.Files.Add(file);

        return file;
    }

    public async Task<bool> SaveChangesAsync()
    {
        var hasSaved = await _context.SaveChangesAsync();

        return (hasSaved >= 0);
    }
}