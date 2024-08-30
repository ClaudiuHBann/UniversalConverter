namespace Shared.Responses
{
public class TemperatureResponse
() : BaseResponse(EType.Temperature)
{
    public required List<double> Temperatures { get; init; }
}
}
