namespace Shared.Responses
{
public class CommonResponse
(Dictionary<string, List<string>> fromToAll) : BaseResponse
{
    public Dictionary<string, List<string>> FromToAll { get; set; } = fromToAll;
}
}
