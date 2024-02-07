namespace Server.Responses
{
public class CurrencyResponse
(List<decimal> money) : BaseResponse
{
    public List<decimal> Money { get; set; } = money;
}
}
