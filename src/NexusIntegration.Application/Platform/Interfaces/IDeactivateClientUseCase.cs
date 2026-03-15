using NexusIntegration.Application.Platform.Dtos;

namespace NexusIntegration.Application.Platform.Interfaces;

public interface IDeactivateClientUseCase
{
    Task<CreateApiClientResponseDto> ExecuteAsync(Guid id);
}
