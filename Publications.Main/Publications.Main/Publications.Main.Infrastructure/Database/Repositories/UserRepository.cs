using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Domain.Entities;
using System.Linq.Expressions;

namespace Publications.Main.Infrastructure.Database.Repositories;

public class UserRepository(AppDbContext context) : CrudRepository<User>(context), IUserRepository
{
    protected override IEnumerable<Expression<Func<User, object>>> Includes { get; } = [x => x.Publications, x => x.Favourites,];
}
