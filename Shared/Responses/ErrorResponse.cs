using System.Net;

using Shared.Exceptions;

namespace Shared.Responses
{
public class ErrorResponse
() : BaseResponse(EType.Error)
{
    public required HttpStatusCode Code { get; init; }
    public required string Message { get; init; }
    public required BaseException.EType TypeException { get; init; }
}
}
