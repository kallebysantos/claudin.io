using CloudIn.Domains.Data.Models;

namespace CloudIn.Contexts.Files.GraphQl;

public class FileQuery
{
    public FileModel GetFile() => 
    (
        new FileModel
        {
            Id = Guid.NewGuid(),
            Name = "TestFile.txt",
            PhysicalPath = "/Files",
            MimeType = "text",
        }
    );
}