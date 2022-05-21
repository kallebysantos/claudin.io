using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQl;

namespace CloudIn.Contexts.Files;

[ExtendObjectType(typeof(RootQuery))]
public class FilesQuery
{

    public IQueryable<FileModel> GetFiles(DataContext context) => context.Files;
}