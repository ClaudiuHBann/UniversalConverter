using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class LinkZipUCService : BaseUCService<LinkZipRequest, LinkZipResponse>
{
    protected override string GetControllerName() => "LinkZip";
}
}
