namespace Shared.Responses
{
public class RadixResponse
() : BaseResponse(EType.Radix)
{
    public required List<string> Numbers { get; init; }
}
}
