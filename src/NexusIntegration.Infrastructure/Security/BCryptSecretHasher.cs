
using NexusIntegration.Domain.Platform.ApiClients;

namespace NexusIntegration.Infrastructure.Security;

public class BCryptSecretHasher : IClientSecretHasher
{
    public string Hash(string secret) => BCrypt.Net.BCrypt.HashPassword(secret);

    public bool Verify(string secret, string hash) => BCrypt.Net.BCrypt.Verify(secret, hash);
}
