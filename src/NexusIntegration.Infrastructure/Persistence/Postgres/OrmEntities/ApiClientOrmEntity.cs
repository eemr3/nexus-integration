
namespace NexusIntegration.Infrastructure.Persistence.Postgres.OrmEntities;

public class ApiClientOrmEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecretHash { get; set; } = string.Empty;
    public List<string> AllowedScopes { get; set; } = [];
    public List<string> AllowedOrigins { get; set; } = [];
    public DateTime? LastUsedAt { get; set; }
    public int RateLimitPerMinute { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
