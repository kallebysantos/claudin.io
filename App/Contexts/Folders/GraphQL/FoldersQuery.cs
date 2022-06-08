using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQL;

namespace CloudIn.Contexts.Folders.GraphQL;

[ExtendObjectType(typeof(RootQuery))]
public class FoldersQuery
{
    public IQueryable<FolderModel> GetFolders([Service] DataContext context) => context.Folders;
}