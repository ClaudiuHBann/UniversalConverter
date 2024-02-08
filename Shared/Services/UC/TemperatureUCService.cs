using Shared.Requests;
using Shared.Responses;
using Shared.Services.UC;

namespace Test.Services.UC
{
public class TemperatureUCService : BaseUCService<TemperatureRequest, TemperatureResponse>
{
    protected override string GetControllerName() => "Temperature";
}
}
