namespace Server.Responses
{
public class FromToResponse
(List<string> fromTo) : BaseResponse
{
    public List<string> FromTo { get; set; } = fromTo;
}
}
