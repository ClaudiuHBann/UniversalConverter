using Shared.Services;
using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

namespace API.Services
{
public abstract class BaseService<Request, Response>() : IService
    where Request : BaseRequest
    where Response : BaseResponse
{
    public abstract Task<List<string>> FromTo();

    protected virtual async Task Validate(Request request)
    {
        var fromTo = await FromTo();

        if (!fromTo.Any(ft => ft.Equals(request.From, StringComparison.CurrentCultureIgnoreCase)))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.Any(ft => ft.Equals(request.To, StringComparison.CurrentCultureIgnoreCase)))
        {
            throw new FromToException(this, false);
        }
    }
    public abstract Task<Response> Convert(Request request);
}
}
