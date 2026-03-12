using System.Net;

namespace NexusIntegration.Shared.Exceptions;

/// <summary>
/// Conflito (ex.: duplicidade de recurso) → 409 Conflict.
/// </summary>
public class ConflictException : ApiException
{
    public ConflictException(string message, string? errorCode = null, Exception? innerException = null)
        : base(HttpStatusCode.Conflict, message, errorCode, innerException) { }
}
