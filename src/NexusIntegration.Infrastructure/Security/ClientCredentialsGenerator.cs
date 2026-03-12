using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NexusIntegration.Domain.Platform.ApiClients;

namespace NexusIntegration.Infrastructure.Security;

public class ClientCredentialsGenerator : IClientCredentialsGenerator
{
    public string GenerateClientId() =>
        Convert.ToHexString(RandomNumberGenerator.GetBytes(16)).ToLower();

    public string GenerateClientSecret() =>
        Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLower();
}
