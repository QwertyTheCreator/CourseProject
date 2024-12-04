using Publications.Auth.Entities;
using Publications.Main.Application.Abstractions.Repositories;

namespace Publications.Auth.Database.Repositories.Abstractions;

public interface IUserRepository : ICrudRepository<User>
{
    Task<User?> GetByUsername(string username);
}
