using Shared.Requests;
using Shared.Responses;

namespace API.Services
{
public class LinkZipService : BaseService<LinkZipRequest, LinkZipResponse>
{
    public override async Task<List<string>> FromTo() =>
        await Task.FromResult<List<string>>(["Shortifier", "Longifier"]);

    public override async Task<LinkZipResponse> Convert(LinkZipRequest request)
    {
        await Validate(request);

        return new([]);
    }
}
}
