namespace Shared.Responses
{
public class CurrencyResponse
(List<decimal> money) : BaseResponse(EType.Currency)
{
    public List<decimal> Money { get; set; } = money;
}
}
