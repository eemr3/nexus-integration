namespace NexusIntegration.Domain.ApiClients;

public class ApiClients
{
    public Guid Id {get; private set;}
    public string Name {get; private set;}
    public string ClientId {get; private set;}
    public string ClientSecretHash {get; private set;}
    public IReadOnlyCollection<string> AllowedScopes {get; private set;}
    public IReadOnlyCollection<string>? AllowedOrigins {get; private set;}
    public DateTime? LastUsedAt {get; private set;}
    public int RateLimitPerMinute {get; private set;}
    public bool Active {get; private set;}
    public DateTime CreatedAt {get; internal set;}
    public DateTime UpdatedAt {get;  set;}

    public void Activate(){
        Active = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate(){
        Active = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RotateSecret(string newSecretHash){
        ClientSecretHash = newSecretHash;
        UpdatedAt = DateTime.UtcNow;
    }
}
