using System.Net;

using Shared.Services;
using Shared.Responses;

namespace Shared.Exceptions
{
public class FromToException : BaseException
{
    public FromToException(ErrorResponse error) : base(EType.FromTo, error)
    {
    }

    public FromToException(IService service, bool fromIsInvalid)
        : base(EType.FromTo, new() { Code = HttpStatusCode.BadRequest, Message = Format(service, fromIsInvalid),
                                     TypeException = EType.FromTo })
    {
    }

    private static string Format(IService service, bool fromIsInvalid)
    {
        var serviceName = service.GetType().Name.Replace("Service", null);
        var fromOrTo = fromIsInvalid ? "from" : "to";

        return $"{serviceName}'s {fromOrTo} is invalid!";
    }
}
}
