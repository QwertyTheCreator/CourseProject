using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.Abstractions.Repositories;

public interface IPublicationRepository : ICrudRepository<Publication>
{
    Task<List<Publication>> GetPaged(int page, int pageSize);
}
