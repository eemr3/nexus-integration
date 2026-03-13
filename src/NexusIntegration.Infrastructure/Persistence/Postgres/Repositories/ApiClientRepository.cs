using Microsoft.EntityFrameworkCore;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Infrastructure.Mappers;

namespace NexusIntegration.Infrastructure.Persistence.Postgres.Repositories;

public class ApiClientRepository : IApiClientRepository
{
    private readonly AppDbContext _context;

    public ApiClientRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ApiClientEntity> CreateAsync(ApiClientEntity apiClient)
    {
        var ormEntity = apiClient.ToOrm();
        _context.ApiClients.Add(ormEntity);
        await _context.SaveChangesAsync();
        return apiClient;
    }

    public async Task<ApiClientEntity?> GetByClientIdAsync(string clientId)
    {
        var result = await _context.ApiClients.FirstOrDefaultAsync(x => x.ClientId == clientId);
        return result is null ? null : result.ToDomain();
    }

    public async Task<ApiClientEntity?> GetByIdAsync(Guid id)
    {
        var result = await _context.ApiClients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return result is null ? null : result.ToDomain();
    }

    public async Task<ApiClientEntity> SaveAsync(ApiClientEntity apiClient)
    {
        var ormEntity = apiClient.ToOrm();
        _context.ApiClients.Update(ormEntity);

        await _context.SaveChangesAsync();
        return apiClient;
    }
}
