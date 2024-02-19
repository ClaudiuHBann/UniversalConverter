namespace Shared.Requests
{
public class BaseRequest
{
    public string From { get; set; } = "";
    public string To { get; set; } = "";

    protected BaseRequest()
    {
    }

    protected BaseRequest(BaseRequest request)
    {
        From = request.From;
        To = request.To;
    }
}
}
