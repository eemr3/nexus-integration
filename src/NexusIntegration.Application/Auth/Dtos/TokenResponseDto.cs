namespace NexusIntegration.Application.Auth.Dtos;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}
