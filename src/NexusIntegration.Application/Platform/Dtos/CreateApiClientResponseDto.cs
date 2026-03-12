namespace NexusIntegration.Application.Platform.Dtos;

public record CreateApiClientResponseDto(
    Guid Id,
    string ClientId,
    string ClientSecret
);