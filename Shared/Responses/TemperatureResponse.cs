namespace Shared.Responses
{
public class TemperatureResponse
(List<double> temperatures) : BaseResponse(EType.Temperature)
{
    public List<double> Temperatures { get; set; } = temperatures;
}
}
