using Shared.Utilities;

namespace Shared.Requests
{
public class LinkZipRequest : BaseRequest
{
    public List<string> URLs { get; set; } = [];

    public LinkZipRequest()
    {
    }

    public LinkZipRequest(LinkZipRequest request) : base(request)
    {
        URLs = request.URLs;
    }

    public override string ToString()
    {
        return this.ToJSON(true);
    }
}
}
