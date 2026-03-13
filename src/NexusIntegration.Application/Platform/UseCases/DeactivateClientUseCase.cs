using NexusIntegration.Application.Platform.Dtos;
using NexusIntegration.Application.Platform.Interfaces;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Shared.Exceptions;

namespace NexusIntegration.Application.Platform.UseCases;

public class DeactivateClientUseCase : IDeactivateClientUseCase
{
    private readonly IApiClientRepository _repository;

    public DeactivateClientUseCase(IApiClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateApiClientResponseDto> ExecuteAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("ApiClient", id.ToString());

        client.Deactivate();
        await _repository.SaveAsync(client);

        return new CreateApiClientResponseDto(
            client.Id,
            client.ClientId,
            client.ClientSecretHash
        );
    }
}
