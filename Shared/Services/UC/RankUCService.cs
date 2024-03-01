using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class RankUCService : BaseUCService<RankRequest, RankResponse>
{
    protected override string GetControllerName() => "Rank";
}
}
