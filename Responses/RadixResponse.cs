namespace Server.Responses
{
public class RadixResponse
(List<string> numbers) : BaseResponse
{
    public List<string> Numbers { get; set; } = numbers;
}
}
