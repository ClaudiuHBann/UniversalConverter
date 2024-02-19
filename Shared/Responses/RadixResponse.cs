namespace Shared.Responses
{
public class RadixResponse
(List<string> numbers) : BaseResponse(EType.Radix)
{
    public List<string> Numbers { get; set; } = numbers;
}
}
