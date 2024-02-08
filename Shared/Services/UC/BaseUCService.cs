﻿using System.Net.Http.Json;

using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Shared.Services.UC
{
public abstract class BaseUCService<TRequest, TResponse>
    where TRequest : BaseRequest
    where TResponse : BaseResponse
{
    private readonly HttpClient _client = new();

#if DEBUG
    private const string _URLBase = "https://localhost:7212/";
#else
    private const string _URLBase = "https://162.55.32.18:80/";
#endif

    protected abstract string GetControllerName();

    protected enum EDBAction
    {
        FromTo,
        Convert
    }

    protected enum EHTTPRequest
    {
        Post,
        Get,
        Put,
        Delete
    }

    public virtual async Task<TResponse> FromTo(TRequest request) => await Request(EHTTPRequest.Get,
                                                                                   EDBAction.FromTo.ToString(),
                                                                                   request);
    public virtual async Task<TResponse> Convert(TRequest request) => await Request(EHTTPRequest.Post,
                                                                                    EDBAction.Convert.ToString(),
                                                                                    request);

    protected async Task<TResponse> Request(EHTTPRequest requestHTTP, string action, object? value = null)
    {
        var uri = $"{_URLBase}{GetControllerName()}/{action}";

        HttpResponseMessage? response = requestHTTP switch {
            EHTTPRequest.Post => await _client.PostAsJsonAsync(uri, value),
            EHTTPRequest.Get => await _client.GetAsJsonAsync(uri, value),
            EHTTPRequest.Put => await _client.PutAsJsonAsync(uri, value),
            EHTTPRequest.Delete => await _client.DeleteAsJsonAsync(uri, value),
            _ => throw new ArgumentException($"The EHTTPRequest type '{requestHTTP}' is not allowed!"),
        };

        return await ProcessResponse(response);
    }

    private static async Task<TResponse> ProcessResponse(HttpResponseMessage message)
    {
        var parseException = new ArgumentException("Could not ReadFromJsonAsync the message content!");
        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync<TResponse>() ?? throw parseException;
        }
        else
        {
            var error = await message.Content.ReadFromJsonAsync<ErrorResponse>() ?? throw parseException;

            var exceptionName = $"Shared.Exceptions.{error.TypeException}Exception";
            throw (Exception)Activator.CreateInstance(Type.GetType(exceptionName)!, error)!;
        }
    }
}
}