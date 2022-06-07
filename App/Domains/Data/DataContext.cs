using CloudIn.Domains.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudIn.Domains.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options): base (options)
    {
    }

    public DbSet<FileModel> Files { get; set; } = null!;
    public DbSet<FolderModel> Folders { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}