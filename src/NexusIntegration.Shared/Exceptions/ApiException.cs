using System.Net;

namespace NexusIntegration.Shared.Exceptions;

/// <summary>
/// Exceção base para erros da API. O middleware de exceção mapeia para o status HTTP correspondente.
/// </summary>
public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ErrorCode { get; }

    protected ApiException(
        HttpStatusCode statusCode,
        string message,
        string? errorCode = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
