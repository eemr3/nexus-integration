namespace NexusIntegration.Domain.Platform.ApiClients;

public class ApiClientEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string ClientId { get; private set; } = string.Empty;

    public string ClientSecretHash { get; private set; } = string.Empty;

    public IReadOnlyCollection<string> AllowedScopes { get; private set; } = [];

    public IReadOnlyCollection<string> AllowedOrigins { get; private set; } = [];

    public DateTime? LastUsedAt { get; private set; }

    public int RateLimitPerMinute { get; private set; }

    public bool Active { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private ApiClientEntity() { } // necessário para ORM / mapper

    private ApiClientEntity(
        Guid id,
        string name,
        string clientId,
        string clientSecretHash,
        IEnumerable<string> allowedScopes,
        IEnumerable<string> allowedOrigins,
        DateTime? lastUsedAt,
        int rateLimitPerMinute,
        bool active,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        Name = name;
        ClientId = clientId;
        ClientSecretHash = clientSecretHash;
        AllowedScopes = allowedScopes.ToList().AsReadOnly();
        AllowedOrigins = allowedOrigins.ToList().AsReadOnly();
        LastUsedAt = lastUsedAt;
        RateLimitPerMinute = rateLimitPerMinute;
        Active = active;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }


    public static ApiClientEntity Reconstitute(
       Guid id,
       string name,
       string clientId,
       string clientSecretHash,
       IEnumerable<string> allowedScopes,
       IEnumerable<string> allowedOrigins,
       DateTime? lastUsedAt,
       int rateLimitPerMinute,
       bool active,
       DateTime createdAt,
       DateTime updatedAt)
    {
        return new ApiClientEntity(
            id, name, clientId, clientSecretHash,
            allowedScopes, allowedOrigins, lastUsedAt,
            rateLimitPerMinute, active, createdAt, updatedAt
        );
    }
    public static ApiClientEntity Create(
        string name,
        string clientId,
        string clientSecretHash,
        IEnumerable<string> allowedScopes,
        IEnumerable<string> allowedOrigins,
        int rateLimitPerMinute,
        bool active)
    {
        var now = DateTime.UtcNow;

        return new ApiClientEntity(
            Guid.NewGuid(),
            name,
            clientId,
            clientSecretHash,
            allowedScopes,
            allowedOrigins,
            null,
            rateLimitPerMinute,
            active,
            now,
            now
        );
    }

    public void Activate()
    {
        Active = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Active = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RotateSecret(string newSecretHash)
    {
        ClientSecretHash = newSecretHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RegisterUsage()
    {
        LastUsedAt = DateTime.UtcNow;
    }
}