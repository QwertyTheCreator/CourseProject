using Microsoft.EntityFrameworkCore;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Domain.Entities.Abstractions;
using System.Linq.Expressions;

namespace Publications.Main.Infrastructure.Database.Repositories;

public abstract class CrudRepository<TEntity>(AppDbContext dbContext) : ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext _dbContext = dbContext;

    protected abstract IEnumerable<Expression<Func<TEntity, object>>> Includes { get; }

    public Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression) => WithIncludes().AnyAsync(expression);

    public Task<List<TEntity>> GetItemsAsync() => WithIncludes().ToListAsync();

    public Task<List<TEntity>> GetItemsAsync(IEnumerable<Guid> ids) =>
        WithIncludes().Where(x => ids.Contains(x.Id)).ToListAsync();

    public Task<TEntity?> GetByIdAsync(Guid id) => WithIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public void Create(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public void CreateRange(IReadOnlyCollection<TEntity> entities) => _dbContext.Set<TEntity>().AddRange(entities);

    public virtual void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public virtual void UpdateRange(IEnumerable<TEntity> items) => _dbContext.Set<TEntity>().UpdateRange(items);

    public void DeleteRange(IEnumerable<TEntity> items) => _dbContext.Set<TEntity>().RemoveRange(items);

    public virtual async Task<bool> DeleteByIdAsync(Guid id)
    {
        var count = await _dbContext.Set<TEntity>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return count > 0;
    }

    public Task<int> DeleteRangeAsync(IEnumerable<Guid> ids) =>
        _dbContext.Set<TEntity>()
            .Where(x => ids.Contains(x.Id))
            .ExecuteDeleteAsync();

    protected virtual IQueryable<TEntity> WithIncludes()
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (var include in Includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}

