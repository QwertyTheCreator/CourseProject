using Microsoft.EntityFrameworkCore;
using Publications.Auth.DatabasesRepositories.Abstractions;
using Publications.Auth.Entities;
using System.Linq.Expressions;

namespace Publications.Auth.Database.Repositories;

public class RoleRepository(AppDbContext context) : CrudRepository<Role>(context), IRoleRepository
{
    protected override IEnumerable<Expression<Func<Role, object>>> Includes { get; } = [];

    public Task<Role> GetDefaultRole() =>
        context.Roles.FirstAsync(x => x.Title == "default");
}
