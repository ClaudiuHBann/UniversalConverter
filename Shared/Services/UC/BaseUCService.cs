using System.Net.Http.Json;

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
    private const string _URLBase = "http://localhost:32406/";
#else
    private const string _URLBase = "http://162.55.32.18:80/";
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
        Get
    }

    public virtual async Task<FromToResponse> FromTo() => await Request<FromToResponse>(EHTTPRequest.Get,
                                                                                        EDBAction.FromTo.ToString());
    public virtual async Task<TResponse> Convert(TRequest request) => await Request<TResponse>(
        EHTTPRequest.Post, EDBAction.Convert.ToString(), request);

    protected async Task<Response> Request<Response>(EHTTPRequest requestHTTP, string action, object? value = null)
    {
        var uri = $"{_URLBase}{GetControllerName()}/{action}";

        HttpResponseMessage? response = requestHTTP switch {
            EHTTPRequest.Get => await _client.GetAsJsonAsync(uri, value),
            EHTTPRequest.Post => await _client.PostAsJsonAsync(uri, value),
            _ => throw new ArgumentException($"The EHTTPRequest type '{requestHTTP}' is not allowed!"),
        };

        return await ProcessResponse<Response>(response);
    }

    private static async Task<Response> ProcessResponse<Response>(HttpResponseMessage message)
    {
        var parseException = new ArgumentException("Could not ReadFromJsonAsync the message content!");
        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync<Response>() ?? throw parseException;
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
