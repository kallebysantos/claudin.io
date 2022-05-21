namespace CloudIn.Domains.Data.Models;

public class FileModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public string PhysicalPath { get; set; } = null!;

    public virtual FolderModel? Folder { get; set; }
}