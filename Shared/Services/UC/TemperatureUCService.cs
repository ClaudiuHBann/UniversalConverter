using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class TemperatureUCService : BaseUCService<TemperatureRequest, TemperatureResponse>
{
    protected override string GetControllerName() => "Temperature";
}
}
