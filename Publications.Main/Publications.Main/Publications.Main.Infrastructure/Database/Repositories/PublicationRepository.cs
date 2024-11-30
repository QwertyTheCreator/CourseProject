using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Domain.Entities;
using System.Linq.Expressions;

namespace Publications.Main.Infrastructure.Database.Repositories;

public class PublicationRepository(AppDbContext dbContext) : CrudRepository<Publication>(dbContext), IPublicationRepository
{
    protected override IEnumerable<Expression<Func<Publication, object>>> Includes { get; } = [x => x.Owner, x => x.UsersWhoLiked];
}
