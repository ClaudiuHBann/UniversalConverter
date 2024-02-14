using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class RadixUCService : BaseUCService<RadixRequest, RadixResponse>
{
    protected override string GetControllerName() => "Radix";
}
}
