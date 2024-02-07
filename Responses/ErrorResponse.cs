using System.Net;

namespace Server.Responses
{
public class ErrorResponse
(HttpStatusCode code, string message) : BaseResponse
{
    public HttpStatusCode Code { get; set; } = code;
    public string Message { get; set; } = message;
}
}
