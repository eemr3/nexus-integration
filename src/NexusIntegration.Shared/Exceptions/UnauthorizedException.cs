using System.Net;

namespace NexusIntegration.Shared.Exceptions;

/// <summary>
/// Não autenticado ou credenciais inválidas → 401 Unauthorized.
/// </summary>
public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message = "Não autorizado.", string? errorCode = null, Exception? innerException = null)
        : base(HttpStatusCode.Unauthorized, message, errorCode, innerException) { }
}
