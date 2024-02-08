using System.Net;

using Shared.Responses;

namespace Shared.Exceptions
{
public class ValueException : BaseException
{
    public ValueException(ErrorResponse error) : base(EType.Value, error)
    {
    }

    public ValueException(string value) : base(EType.Value, new ErrorResponse(HttpStatusCode.BadRequest, value))
    {
    }

    public ValueException(string value, Exception inner)
        : base(EType.Value, new ErrorResponse(HttpStatusCode.BadRequest, value), inner)
    {
    }
}
}
