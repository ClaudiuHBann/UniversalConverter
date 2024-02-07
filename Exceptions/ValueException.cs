using System.Net;

using Server.Responses;

namespace Server.Exceptions
{
public class ValueException : BaseException
{
    public ValueException(string value) : base(new ErrorResponse(HttpStatusCode.BadRequest, value))
    {
    }

    public ValueException(string value, Exception inner)
        : base(new ErrorResponse(HttpStatusCode.BadRequest, value), inner)
    {
    }
}
}
