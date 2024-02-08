namespace Shared.Requests
{
public class CurrencyRequest : BaseRequest
{
    public List<decimal> Money { get; set; } = [];
}
}
