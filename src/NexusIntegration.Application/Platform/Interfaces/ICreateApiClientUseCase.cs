
using NexusIntegration.Application.Platform.Dtos;

namespace NexusIntegration.Application.Platform.interfaces;

public interface ICreateApiClientUseCase
{
    Task<CreateApiClientResponseDto> CreateAsync(CreateApiClientDto apiClientDto);
}
