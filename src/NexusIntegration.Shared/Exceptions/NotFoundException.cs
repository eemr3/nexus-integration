using System.Net;

namespace NexusIntegration.Shared.Exceptions;

/// <summary>
/// Recurso não encontrado → 404 Not Found.
/// </summary>
public class NotFoundException : ApiException
{
    public NotFoundException(string message, string? errorCode = null, Exception? innerException = null)
        : base(HttpStatusCode.NotFound, message, errorCode, innerException) { }

    /// <summary>
    /// Ex.: throw new NotFoundException("ApiClient", id);
    /// </summary>
    public static NotFoundException For(string resourceName, object key) =>
        new NotFoundException($"{resourceName} com identificador '{key}' não encontrado.");
}
