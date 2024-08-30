namespace Shared.Responses
{
public class FromToResponse
() : BaseResponse(EType.FromTo)
{
    public required List<string> FromTo { get; init; }
    public required string DefaultFrom { get; init; }
    public required string DefaultTo { get; init; }
}
}
