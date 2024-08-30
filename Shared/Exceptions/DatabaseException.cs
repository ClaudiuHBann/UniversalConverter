using Shared.Responses;

namespace Shared.Exceptions
{
public class DatabaseException
(ErrorResponse error) : BaseException(EType.Database, error)
{
}
}
