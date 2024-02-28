using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

using API.Entities;

namespace API.Services
{
public abstract class BaseService<Request, Response> : BaseDbService<Request, Response>
    where Request : BaseRequest
    where Response : BaseResponse
{
    protected BaseService(UCContext context) : base(context)
    {
    }

    public virtual Task<List<string>> FromTo() => throw new NotImplementedException();

    private async Task<string> FindFromTo(string fromTo)
    {
        var fromTos = await FromTo();
        var index = fromTos.FindIndex(ft => ft.Equals(fromTo, StringComparison.OrdinalIgnoreCase));
        return fromTos[index];
    }

    private async Task UpdateRequestsFromTo(Request request)
    {
        request.From = await FindFromTo(request.From);
        request.To = await FindFromTo(request.To);
    }

    protected virtual async Task ConvertValidate(Request request)
    {
        if (request.From.Equals(request.To, StringComparison.OrdinalIgnoreCase))
        {
            throw new FromToException(this, false);
        }

        var fromTo = await FromTo();

        if (!fromTo.Any(ft => ft.Equals(request.From, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.Any(ft => ft.Equals(request.To, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, false);
        }
    }

    protected virtual Task<Response> ConvertInternal(Request request) => throw new NotImplementedException();

    public async Task<Response> Convert(Request request)
    {
        await ConvertValidate(request);
        await UpdateRequestsFromTo(request);
        return await ConvertInternal(request);
    }
}
}
