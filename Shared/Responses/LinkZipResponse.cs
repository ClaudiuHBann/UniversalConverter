using System.Text.Json.Serialization;

namespace Shared.Responses
{
public class LinkZipResponse
() : BaseResponse(EType.LinkZip)
{
    [JsonPropertyName("urls")]
    public required List<string> URLs { get; init; }
}
}
