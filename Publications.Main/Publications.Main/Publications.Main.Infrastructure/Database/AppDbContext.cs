using Microsoft.EntityFrameworkCore;
using Publications.Main.Application.Abstractions.Repositories;
using Publications.Main.Domain.Entities;

namespace Publications.Main.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<Publication> Publications { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
