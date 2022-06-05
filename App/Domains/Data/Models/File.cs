using System.ComponentModel.DataAnnotations;

namespace CloudIn.Domains.Data.Models;

public class FileModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? MimeType { get; set; }

    public string? PhysicalPath { get; set; }

    public virtual FolderModel ParentFolder { get; set; } = null!;
}