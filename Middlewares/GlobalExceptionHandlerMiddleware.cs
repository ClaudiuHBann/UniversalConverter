using System.Net;

using Server.Responses;
using Server.Exceptions;

using Microsoft.AspNetCore.Diagnostics;

namespace Server.Middlewares
{
public class GlobalExceptionHandlerMiddleware
(ILogger<GlobalExceptionHandlerMiddleware> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
                                                CancellationToken cancellationToken)
    {
        _logger.LogError($"Exception: {exception.Message}");

        ErrorResponse response;
        if (exception is BaseException baseException)
        {
            response = baseException.Error;
        }
        else
        {
            response = new(HttpStatusCode.InternalServerError, exception.Message);
        }

        httpContext.Response.StatusCode = (int)response.Code;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
}
