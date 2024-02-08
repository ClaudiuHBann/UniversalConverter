using Shared.Requests;
using Shared.Responses;
using Shared.Services.UC;

namespace Test.Services.UC
{
public class RadixUCService : BaseUCService<RadixRequest, RadixResponse>
{
    protected override string GetControllerName() => "Radix";
}
}
