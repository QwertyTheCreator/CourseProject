using Microsoft.EntityFrameworkCore;
using Publications.Auth.Database;
using Publications.Auth.Database.Repositories;
using Publications.Auth.Database.Repositories.Abstractions;
using Publications.Auth.Entities;
using System.Linq.Expressions;

namespace Publications.Main.Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context) : CrudRepository<User>(context), IUserRepository
{
    protected override IEnumerable<Expression<Func<User, object>>> Includes { get; } = [x => x.Role];

    public Task<User?> GetByUsername(string username) =>
        WithIncludes().FirstOrDefaultAsync(x => x.Login == username);
}
