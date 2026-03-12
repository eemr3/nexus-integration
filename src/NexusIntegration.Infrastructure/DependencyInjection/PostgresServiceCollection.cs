using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Infrastructure.Persistence.Postgres;
using NexusIntegration.Infrastructure.Persistence.Postgres.Repositories;

namespace NexusIntegration.Infrastructure.DependencyInjection;

public static class PostgresServiceCollection
{
    public static IServiceCollection AddPostgres(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApiClientRepository, ApiClientRepository>();

        return services;
    }
}
