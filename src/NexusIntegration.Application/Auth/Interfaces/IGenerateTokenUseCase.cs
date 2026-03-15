using NexusIntegration.Application.Auth.Dtos;

namespace NexusIntegration.Application.Auth.Interfaces;

public interface IGenerateTokenUseCase
{
    Task<TokenResponseDto> ExecuteAsync(TokenRequestDto request);
}
