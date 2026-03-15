using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusIntegration.Domain.Integrations.Ports;
using NexusIntegration.Infrastructure.Oracle.DynamicQuery;
using NexusIntegration.Infrastructure.Oracle.Whitelist;

namespace NexusIntegration.Infrastructure.DependencyInjection;

public static class OracleExtensions
{
    public static IServiceCollection AddOracleInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OracleConnection")
            ?? throw new InvalidOperationException("Connection string 'Oracle' não configurada.");

        services.AddSingleton<IWhitelistService, WhitelistService>();
        services.AddSingleton<IDynamicQueryEngine, OracleDynamicQueryEngine>();
        services.AddScoped<IDynamicQueryExecutor, OracleDynamicQueryExecutor>(_ => new OracleDynamicQueryExecutor(connectionString));

        return services;
    }
}
