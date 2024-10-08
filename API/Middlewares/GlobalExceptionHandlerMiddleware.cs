﻿using System.Net;

using Shared.Responses;
using Shared.Exceptions;

namespace API.Middlewares
{
public class GlobalExceptionHandlerMiddleware
(ILogger<GlobalExceptionHandlerMiddleware> logger) : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException exception)
        {
            _logger.LogError($"Exception: {exception.Message}");

            context.Response.StatusCode = (int)exception.Error.Code;
            await context.Response.WriteAsJsonAsync(exception.Error);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Exception: {exception.Message}");

            ErrorResponse error = new() { Code = HttpStatusCode.InternalServerError, Message = exception.Message,
                                          TypeException = BaseException.EType.Unknown };
            context.Response.StatusCode = (int)error.Code;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
}
