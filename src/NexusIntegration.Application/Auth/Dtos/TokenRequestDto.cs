namespace NexusIntegration.Application.Auth.Dtos;

public class TokenRequestDto
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
