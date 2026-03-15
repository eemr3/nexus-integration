using NexusIntegration.Application.Platform.Dtos;
using NexusIntegration.Application.Platform.interfaces;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Shared.Exceptions;
using NexusIntegration.Shared.Types;

namespace NexusIntegration.Application.Platform.UseCases;

public class CreateApiClientUseCase : ICreateApiClientUseCase
{
    private const int MaxClientIdAttempts = 5;

    private readonly IApiClientRepository _repository;
    private readonly IClientSecretHasher _secretHasher;
    private readonly IClientCredentialsGenerator _generator;

    public CreateApiClientUseCase(
        IApiClientRepository repository,
        IClientSecretHasher secretHasher,
        IClientCredentialsGenerator generator)
    {
        _repository = repository;
        _secretHasher = secretHasher;
        _generator = generator;
    }

    public async Task<CreateApiClientResponseDto> CreateAsync(CreateApiClientDto apiClientDto)
    {
        string clientId;
        var attempt = 0;

        do
        {
            clientId = _generator.GenerateClientId();
            var existing = await _repository.GetByClientIdAsync(clientId);
            if (existing is null)
                break;
            attempt++;
        } while (attempt < MaxClientIdAttempts);

        if (attempt >= MaxClientIdAttempts)
            throw new ConflictException($"Não foi possível gerar um ClientId único. Tente novamente. {MaxClientIdAttempts} tentativas realizadas.");

        var clientSecret = _generator.GenerateClientSecret();
        var clientSecretHash = _secretHasher.Hash(clientSecret);

        var apiClient = ApiClientEntity.Create(
            apiClientDto.Name,
            clientId,
            clientSecretHash,
            apiClientDto.AllowedScopes ?? [],
            apiClientDto.AllowedOrigins ?? [],
            50,
            true
        );

        await _repository.CreateAsync(apiClient);

        return new CreateApiClientResponseDto(
            apiClient.Id,
            clientId,
            clientSecret);
    }
}
