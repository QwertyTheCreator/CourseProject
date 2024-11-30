using Publications.Main.Domain.Entities;

namespace Publications.Main.Application.Abstractions.Repositories;

public interface IPublicationRepository : ICrudRepository<Publication>;
