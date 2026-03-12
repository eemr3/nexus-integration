namespace NexusIntegration.Shared.Types;

/// <summary>
/// Códigos de erro para o cliente da API (frontend/integrador) identificar o tipo do problema.
/// </summary>
public static class ErrorCodes
{
    public const string Validation = "VALIDATION";
    public const string NotFound = "NOT_FOUND";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Conflict = "CONFLICT";
    public const string Internal = "INTERNAL_ERROR";
}
