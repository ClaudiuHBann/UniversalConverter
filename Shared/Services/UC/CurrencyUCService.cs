using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.UC
{
public class CurrencyUCService : BaseUCService<CurrencyRequest, CurrencyResponse>
{
    protected override string GetControllerName() => "Currency";
}
}
