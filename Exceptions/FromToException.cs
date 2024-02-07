using System.Net;

using Server.Services;

namespace Server.Exceptions
{
public class FromToException : BaseException
{
    public FromToException(IService service, bool fromIsInvalid)
        : base(new(HttpStatusCode.BadRequest, Format(service, fromIsInvalid)))
    {
    }

    public FromToException(IService service, bool fromIsInvalid, Exception inner)
        : base(new(HttpStatusCode.BadRequest, Format(service, fromIsInvalid)), inner)
    {
    }

    private static string Format(IService service, bool fromIsInvalid)
    {
        var serviceName = service.ToString()!.Replace("Service", "");
        var fromOrTo = fromIsInvalid ? "from" : "to";

        return $"{serviceName}'s {fromOrTo} is invalid!";
    }
}
}
