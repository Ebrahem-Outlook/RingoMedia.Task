using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Web.Database;

public sealed class AppDbContext :  DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
