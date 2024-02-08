using Shared.Responses;

namespace Shared.Exceptions
{
public class BaseException : Exception
{
    public enum EType : byte
    {
        Unknown,
        FromTo,
        Value
    }

    public ErrorResponse Error { get; }
    public EType Type { get; }

    public BaseException(EType type, ErrorResponse error) : base(error.Message)
    {
        Type = type;

        Error = error;
        Error.TypeException = type;
    }

    public BaseException(EType type, ErrorResponse error, Exception inner) : base(error.Message, inner)
    {
        Type = type;

        Error = error;
        Error.TypeException = type;
    }
}
}
