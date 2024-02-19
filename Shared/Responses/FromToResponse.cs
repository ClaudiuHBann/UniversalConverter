namespace Shared.Responses
{
public class FromToResponse
(List<string> fromTo) : BaseResponse(EType.FromTo)
{
    public List<string> FromTo { get; set; } = fromTo;
}
}
