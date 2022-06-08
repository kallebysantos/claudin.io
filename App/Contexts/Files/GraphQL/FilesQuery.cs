using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQL;

namespace CloudIn.Contexts.Files.GraphQL;

[ExtendObjectType(typeof(RootQuery))]
public class FilesQuery
{

    public IQueryable<FileModel> GetFiles(DataContext context) => context.Files;
}