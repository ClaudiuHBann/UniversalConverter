using System.Text.Json.Serialization;

namespace Shared.Responses
{
public class LinkZipResponse
(List<string> urls) : BaseResponse(EType.LinkZip)
{
    [JsonPropertyName("urls")]
    public List<string> URLs { get; set; } = urls;
}
}
