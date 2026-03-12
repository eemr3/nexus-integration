using System.Net;

namespace NexusIntegration.Shared.Exceptions;

/// <summary>
/// Erro de validação ou requisição inválida → 400 Bad Request.
/// </summary>
public class BadRequestException : ApiException
{
    public BadRequestException(string message, string? errorCode = null, Exception? innerException = null)
        : base(HttpStatusCode.BadRequest, message, errorCode, innerException) { }
}
