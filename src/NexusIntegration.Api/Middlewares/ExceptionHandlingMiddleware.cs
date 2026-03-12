using System.Text.Json;
using NexusIntegration.Shared.Exceptions;
using NexusIntegration.Shared.Types;

namespace NexusIntegration.Api.Middlewares;

/// <summary>
/// Captura exceções e devolve resposta HTTP padronizada (status + corpo JSON).
/// Equivalente ao api-exception.filter do NestJS.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, response) = ex switch
        {
            ApiException apiEx => ((int)apiEx.StatusCode, new ErrorResponse(
                apiEx.Message,
                apiEx.ErrorCode ?? "API_ERROR",
                _env.IsDevelopment() ? ex.StackTrace : null)),
            _ => (StatusCodes.Status500InternalServerError, new ErrorResponse(
                _env.IsDevelopment() ? ex.Message : "Ocorreu um erro interno.",
                ErrorCodes.Internal,
                _env.IsDevelopment() ? ex.StackTrace : null))
        };

        if (statusCode >= 500)
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
        else
            _logger.LogWarning(ex, "Erro de negócio: {Message}", ex.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    private record ErrorResponse(string Message, string ErrorCode, string? StackTrace = null);
}
