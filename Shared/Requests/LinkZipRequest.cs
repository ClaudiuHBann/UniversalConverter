using System.Text.Json.Serialization;

namespace Shared.Requests
{
public class LinkZipRequest : BaseRequest
{
    [JsonPropertyName("urls")]
    public List<string> URLs { get; set; } = [];

    public LinkZipRequest()
    {
    }

    public LinkZipRequest(LinkZipRequest request) : base(request)
    {
        URLs = request.URLs;
    }
}
}
