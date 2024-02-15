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
    public virtual Task<List<string>> FromTo() => throw new NotImplementedException();

    protected virtual async Task ValidateConvert(Request request)
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

    public virtual Task<Response> Convert(Request request) => throw new NotImplementedException();
}
}
