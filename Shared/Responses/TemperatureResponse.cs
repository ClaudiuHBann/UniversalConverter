namespace Shared.Responses
{
public class TemperatureResponse
(List<double> temperatures) : BaseResponse
{
    public List<double> Temperatures { get; set; } = temperatures;
}
}
