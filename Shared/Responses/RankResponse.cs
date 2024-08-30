namespace Shared.Responses
{
public class RankResponse
() : BaseResponse(EType.Rank)
{
    public required List<string> Converters { get; init; }
}
}
