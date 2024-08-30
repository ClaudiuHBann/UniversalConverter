using System.Net;

using Shared.Responses;

namespace Shared.Exceptions
{
public class ValueException : BaseException
{
    public ValueException(ErrorResponse error) : base(EType.Value, error)
    {
    }

    public ValueException(string value)
        : base(EType.Value,
               new ErrorResponse() { Code = HttpStatusCode.BadRequest, Message = value, TypeException = EType.Value })
    {
    }
}
}
