using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexusIntegration.Domain.Platform.ApiClients;

public interface IClientCredentialsGenerator
{
    string GenerateClientId();
    string GenerateClientSecret();
}
