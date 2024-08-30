namespace Shared.Requests
{
public class RankRequest : BaseRequest
{
    public required int Converters { get; init; } = 3;
}
}
