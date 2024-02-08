using System.Net;

using Shared.Exceptions;

namespace Shared.Responses
{
public class ErrorResponse
(HttpStatusCode code, string message) : BaseResponse
{
    public HttpStatusCode Code { get; set; } = code;
    public string Message { get; set; } = message;
    public BaseException.EType TypeException { get; set; } = BaseException.EType.Unknown;
}
}
