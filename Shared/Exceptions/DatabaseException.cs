using Shared.Responses;

namespace Shared.Exceptions
{
public class DatabaseException : BaseException
{
    public DatabaseException(ErrorResponse error) : base(EType.Database, error)
    {
    }

    public DatabaseException(ErrorResponse error, Exception inner) : base(EType.Database, error, inner)
    {
    }
}
}
