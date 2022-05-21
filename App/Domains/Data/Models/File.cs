using System.ComponentModel.DataAnnotations;

namespace CloudIn.Domains.Data.Models;

public class FileModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string MimeType { get; set; } = null!;

    [Required]
    public string PhysicalPath { get; set; } = null!;

    public virtual FolderModel ParentFolder { get; set; } = null!;
}