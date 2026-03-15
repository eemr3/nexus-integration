namespace NexusIntegration.Domain.Security.Interfaces;


public interface ITokenService
{
    string GenerateToken(
        Guid clientId,
        string clientIdentifier,
        IEnumerable<string> scopes
    );
}