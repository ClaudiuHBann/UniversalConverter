using Shared.Requests;
using Shared.Responses;
using Shared.Services.UC;

namespace Test.Services.UC
{
public class CurrencyUCService : BaseUCService<CurrencyRequest, CurrencyResponse>
{
    protected override string GetControllerName() => "Currency";
}
}
