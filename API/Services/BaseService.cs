using Shared.Entities;
using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

using Microsoft.EntityFrameworkCore;

using API.Entities;

namespace API.Services
{
public abstract class BaseService<Request, Response> : BaseDbService<Request, Response>
    where Request : BaseRequest
    where Response : BaseResponse
{
    private readonly UCContext _context;

    protected BaseService(UCContext context) : base(context)
    {
        _context = context;
    }

    public abstract bool IsConverter();
    public abstract string GetServiceName();

    public abstract Task<FromToResponse> FromTo();

    private async Task<string> FindFromTo(string fromTo)
    {
        var fromTos = await FromTo();
        var index = fromTos.FromTo.FindIndex(ft => ft.Equals(fromTo, StringComparison.OrdinalIgnoreCase));
        return fromTos.FromTo[index];
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

        if (!fromTo.FromTo.Any(ft => ft.Equals(request.From, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.FromTo.Any(ft => ft.Equals(request.To, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, false);
        }
    }

    protected abstract Task<Response> ConvertInternal(Request request);

    public async Task<Response> Convert(Request request)
    {
        await ConvertValidate(request);
        await UpdateRequestsFromTo(request);
        var response = await ConvertInternal(request);
        await UpdateRank();
        return response;
    }

    private async Task UpdateRank()
    {

        var converter = GetServiceName();
        var rank = await _context.Ranks.FirstOrDefaultAsync(rank => rank.Converter == converter) ??
                   await Create(new RankEntity(converter));

        // TODO: increase but update once 5 minutes or smth
        rank.Conversions++;
        await Update(rank);
    }
}
}
