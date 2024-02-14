namespace Shared.Responses
{
public class LinkZipResponse
(List<string> urls) : BaseResponse
{
    public List<string> URLs { get; set; } = urls;
}
}
