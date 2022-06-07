using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQl;

namespace CloudIn.Contexts.Users;

[ExtendObjectType(typeof(RootQuery))]
public class UsersQuery
{
    public IQueryable<User> GetUsers([Service] DataContext context) => context.Users;
}