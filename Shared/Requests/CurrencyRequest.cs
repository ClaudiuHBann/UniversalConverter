namespace Shared.Requests
{
public class CurrencyRequest : BaseRequest
{
    public List<decimal> Money { get; set; } = [];

    public CurrencyRequest()
    {
    }

    public CurrencyRequest(CurrencyRequest request)
    {
        Money = request.Money;
    }
}
}
