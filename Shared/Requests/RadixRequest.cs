namespace Shared.Requests
{
public class RadixRequest : BaseRequest
{
    public List<string> Numbers { get; set; } = [];

    public RadixRequest()
    {
    }

    public RadixRequest(RadixRequest request)
    {
        Numbers = request.Numbers;
    }
}
}
