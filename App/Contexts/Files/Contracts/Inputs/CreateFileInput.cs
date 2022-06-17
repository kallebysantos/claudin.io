using System.ComponentModel.DataAnnotations;

namespace CloudIn.Contexts.Files.Contracts.Inputs;

public class CreateFileInput
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Guid ParentFolderId { get; set; }
}