using Microsoft.EntityFrameworkCore;
using NexusIntegration.Infrastructure.Persistence.Postgres.OrmEntities;

namespace NexusIntegration.Infrastructure.Persistence.Postgres;

public class AppDbContext : DbContext
{
    public DbSet<ApiClientOrmEntity> ApiClients => Set<ApiClientOrmEntity>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
