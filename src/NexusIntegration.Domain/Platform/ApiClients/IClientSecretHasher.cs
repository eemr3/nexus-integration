using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexusIntegration.Domain.Platform.ApiClients;

public interface IClientSecretHasher
{
    string Hash(string secret);
    bool Verify(string secret, string hash);
}
