namespace Shared.Responses
{
public class LinkZipResponse
(List<string> urls) : BaseResponse(EType.LinkZip)
{
    public List<string> URLs { get; set; } = urls;
}
}
