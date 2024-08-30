namespace Shared.Responses
{
public class CommonResponse
() : BaseResponse(EType.Common)
{
    public required Dictionary<string, FromToResponse> FromToAll { get; init; }
}
}
