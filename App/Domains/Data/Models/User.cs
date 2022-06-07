using System.ComponentModel.DataAnnotations;

namespace CloudIn.Domains.Data.Models;

public class User
{
    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    public virtual ICollection<FolderModel> Folders { get; set; } = null!;
}