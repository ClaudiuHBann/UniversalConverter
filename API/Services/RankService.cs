using API.Entities;

using Microsoft.EntityFrameworkCore;

using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace API.Services
{
public class RankService : BaseService<RankRequest, RankResponse>
{
    private readonly UCContext _context;
    private readonly CommonService _common;

    private readonly SemaphoreSlim _ss = new(1);
    private bool _upToDate = false;

    public RankService(UCContext context, CommonService common) : base(context)
    {
        _context = context;
        _common = common;
    }

    public override bool IsConverter() => false;

    public override string GetServiceName() => "Rank";

    public override Task<FromToResponse> FromTo() => throw new InvalidOperationException();
    protected override Task<RankResponse> ConvertInternal(RankRequest request) => throw new InvalidOperationException();

    private async Task UpdateRanks()
    {
        try
        {
            await _ss.WaitAsync();
            if (_upToDate)
            {
                return;
            }

            var convertersDB = _context.Ranks.Select(rank => rank.Converter);
            var convertersToAdd = _common.FindAllServices()
                                      .Select(service => service.GetType().Name.Replace("Service", null))
                                      .Except(convertersDB)
                                      .Select(converter => new RankEntity(converter))
                                      .ToList();

            foreach (var converter in convertersToAdd)
            {
                await Create(converter);
            }

            _upToDate = true;
        }
        finally
        {
            _ss.Release();
        }
    }

    public async Task<RankResponse> Converters(RankRequest request)
    {
        await UpdateRanks();

        var converters = await _context.Ranks.OrderByDescending(rank => rank.Conversions)
                             .Take(request.Converters)
                             .Select(rank => rank.Converter)
                             .ToListAsync();

        return new RankResponse() { Converters = converters };
    }
}
}
