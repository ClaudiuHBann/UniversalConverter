namespace Shared.Responses
{
public class CommonResponse
(Dictionary<string, List<string>> fromToAll) : BaseResponse(EType.Common)
{
    public Dictionary<string, List<string>> FromToAll { get; set; } = fromToAll;
}
}
