namespace Shared.Responses
{
public class CommonResponse
(Dictionary<string, FromToResponse> fromToAll) : BaseResponse(EType.Common)
{
    public Dictionary<string, FromToResponse> FromToAll { get; set; } = fromToAll;
}
}
