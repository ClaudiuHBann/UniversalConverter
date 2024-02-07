using Server.Responses;

namespace Server.Exceptions
{
public class BaseException : Exception
{
    public BaseException(ErrorResponse error) : base(error.Message)
    {
        Error = error;
    }

    public BaseException(ErrorResponse error, Exception inner) : base(error.Message, inner)
    {
        Error = error;
    }

    public ErrorResponse Error { get; }
}
}
