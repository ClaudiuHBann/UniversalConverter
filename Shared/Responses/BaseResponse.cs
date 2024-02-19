namespace Shared.Responses
{
public class BaseResponse
{
    public enum EType
    {
        Unknown,
        Common,
        Currency,
        Error,
        FromTo,
        LinkZip,
        Radix,
        Temperature
    }

    public EType Type { get; set; }

    protected BaseResponse(EType type)
    {
        Type = type;
    }
}
}
