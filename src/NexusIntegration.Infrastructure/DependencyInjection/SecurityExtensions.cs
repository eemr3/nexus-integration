using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Domain.Security.Interfaces;
using NexusIntegration.Infrastructure.Auth;
using NexusIntegration.Infrastructure.Security;

namespace NexusIntegration.Infrastructure.DependencyInjection;

public static class SecurityExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientSecretHasher, BCryptSecretHasher>();
        services.AddScoped<IClientCredentialsGenerator, ClientCredentialsGenerator>();

        var jwtSettings = configuration
            .GetSection("Jwt")
            .Get<JwtSettings>()
            ?? throw new InvalidOperationException("Jwt settings not configured");

        services.AddSingleton(jwtSettings);

        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
