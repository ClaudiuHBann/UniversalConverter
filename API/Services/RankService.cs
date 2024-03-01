using API.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Requests;
using Shared.Responses;

namespace API.Services
{
public class RankService : BaseService<RankRequest, RankResponse>
{
    private readonly UCContext _context;

    public RankService(UCContext context) : base(context)
    {
        _context = context;
    }

    public override bool IsConverter() => false;

    public override Task<FromToResponse> FromTo() => throw new InvalidOperationException();
    protected override Task<RankResponse> ConvertInternal(RankRequest request) => throw new InvalidOperationException();

    public async Task<RankResponse> Converters(RankRequest request)
    {
        var converters = await _context.Ranks.OrderBy(rank => rank.Conversions)
                             .Take(request.Converters)
                             .Select(rank => rank.Converter)
                             .ToListAsync();

        return new RankResponse(converters);
    }
}
}
