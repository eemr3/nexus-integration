using NexusIntegration.Domain.ApiClients;

namespace NexusIntegration.Application.Interfaces.Repositories;

public interface  IApiClientsRepository
{
    Task<ApiClientsEntity> GetByIdAsync(Guid id);
    Task<ApiClientsEntity> GetByClientIdAsync(string clientId);
    Task<ApiClientsEntity> CreateAsync(ApiClientsEntity apiClient);
    Task<ApiClientsEntity> SaveAsync(ApiClientsEntity apiClient);
}
