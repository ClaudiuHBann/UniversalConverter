namespace Shared.Requests
{
public class BaseRequest
{
    public string From { get; set; } = "";
    public string To { get; set; } = "";

    public BaseRequest()
    {
    }

    public BaseRequest(BaseRequest request)
    {
        From = request.From;
        To = request.To;
    }
}
}
