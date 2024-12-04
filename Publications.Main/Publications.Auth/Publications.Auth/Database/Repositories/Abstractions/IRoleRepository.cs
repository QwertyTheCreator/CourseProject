using Publications.Auth.Entities;
using Publications.Main.Application.Abstractions.Repositories;

namespace Publications.Auth.DatabasesRepositories.Abstractions;

public interface IRoleRepository : ICrudRepository<Role>
{
    public Task<Role> GetDefaultRole();
}
