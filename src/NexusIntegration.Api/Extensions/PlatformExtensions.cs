using NexusIntegration.Application.Auth.Interfaces;
using NexusIntegration.Application.Auth.UseCases;
using NexusIntegration.Application.Platform.interfaces;
using NexusIntegration.Application.Platform.Interfaces;
using NexusIntegration.Application.Platform.UseCases;

namespace NexusIntegration.Api.Extensions;

public static class PlatformExtensions
{
    public static IServiceCollection AddPlatformApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateApiClientUseCase, CreateApiClientUseCase>();
        services.AddScoped<IDeactivateClientUseCase, DeactivateClientUseCase>();
        services.AddScoped<IGenerateTokenUseCase, GenerateTokenUseCase>();
        services.AddScoped<IRotateSecretUseCase, RotateSecretUseCase>();

        return services;
    }
}
