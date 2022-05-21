namespace CloudIn.Domains.Data.Models;

public class FolderModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FileModel> Files { get; set; } = null!;

    public virtual ICollection<FolderModel> Folders { get; set; } = null!;
}