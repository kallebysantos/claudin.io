namespace CloudIn.Domains.Data.Models;

public class File
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public string PhysicalPath { get; set; } = null!;
}