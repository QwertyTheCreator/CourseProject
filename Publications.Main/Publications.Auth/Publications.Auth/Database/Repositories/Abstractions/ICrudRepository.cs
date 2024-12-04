using Publications.Auth.Entities;
using System.Linq.Expressions;

namespace Publications.Main.Application.Abstractions.Repositories;

public interface ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<List<TEntity>> GetItemsAsync();
    Task<List<TEntity>> GetItemsAsync(IEnumerable<Guid> ids);
    Task<TEntity?> GetByIdAsync(Guid id);
    void Create(TEntity entity);
    void CreateRange(IReadOnlyCollection<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> items);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<int> DeleteRangeAsync(IEnumerable<Guid> ids);
    void DeleteRange(IEnumerable<TEntity> items);
}