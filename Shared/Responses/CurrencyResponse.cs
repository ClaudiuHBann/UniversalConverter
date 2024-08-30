namespace Shared.Responses
{
public class CurrencyResponse
() : BaseResponse(EType.Currency)
{
    public required List<decimal> Money { get; init; }
}
}
