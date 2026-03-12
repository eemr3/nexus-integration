using NexusIntegration.Application.Platform.interfaces;
using NexusIntegration.Application.Platform.UseCases;

namespace NexusIntegration.Api.Extensions;

public static class PlatformExtensions
{
    public static IServiceCollection AddPlatformApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateApiClientUseCase, CreateApiClientUseCase>();

        return services;
    }
}
