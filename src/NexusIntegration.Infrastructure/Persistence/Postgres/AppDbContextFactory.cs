
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NexusIntegration.Infrastructure.Persistence.Postgres;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsHistoryTable("__ef_migrations", "public")
                       .MigrationsAssembly("NexusIntegration.Infrastructure")
            )
            .Options;

        return new AppDbContext(options);
    }
}
