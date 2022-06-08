using CloudIn.Domains.Data;
using CloudIn.Domains.Data.Models;
using CloudIn.Domains.GraphQL;

namespace CloudIn.Contexts.Users.GraphQL;

[ExtendObjectType(typeof(RootQuery))]
public class UsersQuery
{
    public IQueryable<User> GetUsers([Service] DataContext context) => context.Users;
}