namespace CloudIn.Domains.Data.Models;

public class Folder
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<File> Files { get; set; } = null!;

    public virtual ICollection<Folder> Folders { get; set; } = null!;
}