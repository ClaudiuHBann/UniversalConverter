using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class CommonUCService : BaseUCService<CommonRequest, CommonResponse>
{
    protected override string GetControllerName() => "Common";

    public async Task<CommonResponse> FromToAll() => await Request<CommonResponse>(EHTTPRequest.Get, nameof(FromToAll));
}
}
