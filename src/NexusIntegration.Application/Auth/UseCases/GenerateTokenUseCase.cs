using NexusIntegration.Application.Auth.Dtos;
using NexusIntegration.Application.Auth.Interfaces;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Domain.Security.Interfaces;
using NexusIntegration.Shared.Exceptions;

namespace NexusIntegration.Application.Auth.UseCases;

public class GenerateTokenUseCase : IGenerateTokenUseCase
{
    private readonly IApiClientRepository _apiClientRepository;
    private readonly ITokenService _tokenService;

    public GenerateTokenUseCase(
        IApiClientRepository apiClientRepository,
        ITokenService tokenService)
    {
        _apiClientRepository = apiClientRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDto> ExecuteAsync(TokenRequestDto request)
    {
        var client = await _apiClientRepository.GetByClientIdAsync(request.ClientId)
        ?? throw new UnauthorizedException("Credenciais inválidas");

        if (client.ClientSecretHash != request.ClientSecret)
            throw new UnauthorizedException("Credenciais inválidas");

        var token = _tokenService.GenerateToken(client.Id, client.ClientId, client.AllowedScopes);

        return new TokenResponseDto
        {
            AccessToken = token,
            ExpiresIn = 2600
        };
    }

}