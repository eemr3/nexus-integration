namespace NexusIntegration.Domain.Platform.ApiClients;

public interface IApiClientRepository
{
    Task<ApiClientEntity?> GetByIdAsync(Guid id);
    Task<ApiClientEntity?> GetByClientIdAsync(string clientId);
    Task<ApiClientEntity> CreateAsync(ApiClientEntity apiClient);
    Task<ApiClientEntity> SaveAsync(ApiClientEntity apiClient);
}
