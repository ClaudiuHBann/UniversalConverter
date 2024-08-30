namespace Shared.Responses
{
public class BaseResponse
{
    public enum EType
    {
        Unknown,
        Common,
        Rank,
        Currency,
        Error,
        FromTo,
        LinkZip,
        Radix,
        Temperature
    }

    public EType Type { get; }

    protected BaseResponse(EType type)
    {
        Type = type;
    }
}
}
