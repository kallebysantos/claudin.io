using System.ComponentModel.DataAnnotations;

namespace CloudIn.Domains.Data.Models;

public class FolderModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual FolderModel ParentFolder { get; set; } = null!;

    public virtual ICollection<FileModel> Files { get; set; } = null!;

    public virtual ICollection<FolderModel> Folders { get; set; } = null!;
}