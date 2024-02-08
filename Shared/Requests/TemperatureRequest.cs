namespace Shared.Requests
{
public class TemperatureRequest : BaseRequest
{
    public List<double> Temperatures { get; set; } = [];

    public TemperatureRequest()
    {
    }

    public TemperatureRequest(TemperatureRequest request)
    {
        Temperatures = request.Temperatures;
    }
}
}
