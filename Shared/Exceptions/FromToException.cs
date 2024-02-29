using System.Net;

using Shared.Responses;
using Shared.Services;

namespace Shared.Exceptions
{
public class FromToException : BaseException
{
    public FromToException(ErrorResponse error) : base(EType.FromTo, error)
    {
    }

    public FromToException(IService service, bool fromIsInvalid)
        : base(EType.FromTo, new(HttpStatusCode.BadRequest, Format(service, fromIsInvalid)))
    {
    }

    public FromToException(IService service, bool fromIsInvalid, Exception inner)
        : base(EType.FromTo, new(HttpStatusCode.BadRequest, Format(service, fromIsInvalid)), inner)
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
