using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQl;

namespace CloudIn.Contexts.Folders;

[ExtendObjectType(typeof(RootQuery))]
public class FoldersQuery
{
    public IQueryable<FolderModel> GetFolders([Service] DataContext context) => context.Folders;
}