namespace NexusIntegration.Application.Platform.Dtos;

public class CreateApiClientDto
{
    public string Name { get; set; } = string.Empty;
    public List<string>? AllowedScopes { get; set; } = [];
    public List<string>? AllowedOrigins { get; set; } = [];
}
