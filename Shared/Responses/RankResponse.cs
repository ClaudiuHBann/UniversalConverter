namespace Shared.Responses
{
public class RankResponse : BaseResponse
{
    public List<string> Converters { get; set; }

    public RankResponse(List<string> converters) : base(EType.Rank)
    {
        Converters = converters;
    }
}
}
