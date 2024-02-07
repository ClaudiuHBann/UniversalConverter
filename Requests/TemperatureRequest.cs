namespace Server.Requests
{
public class TemperatureRequest : BaseRequest
{
    public List<double> Temperatures { get; set; } = [];
}
}
