using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexusIntegration.Domain.Platform.ApiClients;
using NexusIntegration.Infrastructure.Persistence.Postgres.OrmEntities;

namespace NexusIntegration.Infrastructure.Mappers;

public static class ApiClientMapper
{
    public static ApiClientEntity ToDomain(this ApiClientOrmEntity orm)
    {
        return ApiClientEntity.Reconstitute(
            orm.Id,
            orm.Name,
            orm.ClientId,
            orm.ClientSecretHash,
            [.. orm.AllowedScopes],
            [.. orm.AllowedOrigins],
            orm.LastUsedAt,
            orm.RateLimitPerMinute,
            orm.Active,
            orm.CreatedAt,
            orm.UpdatedAt
        );
    }

    public static ApiClientOrmEntity ToOrm(this ApiClientEntity entity)
    {
        return new ApiClientOrmEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            ClientId = entity.ClientId,
            ClientSecretHash = entity.ClientSecretHash,
            AllowedScopes = [.. entity.AllowedScopes],
            AllowedOrigins = [.. entity.AllowedOrigins],
            LastUsedAt = entity.LastUsedAt,
            RateLimitPerMinute = entity.RateLimitPerMinute,
            Active = entity.Active,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

}
