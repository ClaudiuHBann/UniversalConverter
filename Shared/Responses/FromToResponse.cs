namespace Shared.Responses
{
public class FromToResponse
(List<string> fromTo, string defaultFrom, string defaultTo) : BaseResponse(EType.FromTo)
{
    public List<string> FromTo { get; set; } = fromTo;
    public string DefaultFrom { get; set; } = defaultFrom;
    public string DefaultTo { get; set; } = defaultTo;
}
}
