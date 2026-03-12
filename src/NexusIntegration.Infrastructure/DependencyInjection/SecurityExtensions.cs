using Microsoft.Extensions.DependencyInjection;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Infrastructure.Security;

namespace NexusIntegration.Infrastructure.DependencyInjection;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IClientSecretHasher, BCryptSecretHasher>();
        services.AddScoped<IClientCredentialsGenerator, ClientCredentialsGenerator>();

        return services;
    }
}
