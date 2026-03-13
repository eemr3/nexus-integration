using NexusIntegration.Application.Platform.Dtos;

namespace NexusIntegration.Application.Platform.Interfaces;

public interface IRotateSecretUseCase
{
    Task<CreateApiClientResponseDto> ExecuteAsync(Guid id);
}
