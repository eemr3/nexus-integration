using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexusIntegration.Domain.ApiClients;

public interface  IApiClientsRepository
{
    Task<ApiClients> GetByIdAsync(Guid id);
    Task<ApiClients> GetByClientIdAsync(string clientId);
    Task<ApiClients> CreateAsync(ApiClients apiClient);
    Task<ApiClients> SaveAsync(ApiClients apiClient);
}
