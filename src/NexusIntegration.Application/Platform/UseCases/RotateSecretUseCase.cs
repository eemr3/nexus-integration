using NexusIntegration.Application.Platform.Dtos;
using NexusIntegration.Application.Platform.Interfaces;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Shared.Exceptions;

namespace NexusIntegration.Application.Platform.UseCases;

public class RotateSecretUseCase : IRotateSecretUseCase
{

    private readonly IApiClientRepository _repository;
    private readonly IClientSecretHasher _hasher;
    private readonly IClientCredentialsGenerator _generator;

    public RotateSecretUseCase(
        IApiClientRepository repository,
        IClientSecretHasher hasher,
        IClientCredentialsGenerator generator)
    {
        _repository = repository;
        _hasher = hasher;
        _generator = generator;
    }
    public async Task<CreateApiClientResponseDto> ExecuteAsync(Guid id)
    {
        var client = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado");

        var newSecret = _generator.GenerateClientSecret();
        var newSecretHash = _hasher.Hash(newSecret);

        client.RotateSecret(newSecretHash);
        await _repository.SaveAsync(client);

        return new CreateApiClientResponseDto(
            client.Id,
            client.ClientId,
            newSecret);
    }
}
