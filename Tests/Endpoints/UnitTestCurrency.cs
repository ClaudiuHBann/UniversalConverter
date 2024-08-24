using Tests.Utilities;

using Shared.Requests;
using Shared.Services.UC;

namespace Tests.Endpoints
{
[TestFixture]
internal class UnitTestCurrency : UnitTestBase
{
    private readonly CurrencyUCService _service;

    public UnitTestCurrency()
    {
        _service = DI.GetService<CurrencyUCService>()!;
    }

    // clang-format off
    private static readonly CurrencyRequest _requestValid = new()
    {
        From = "RON",
        To = "USD",
        Money = [69.420m, 420.69m]
    };
    
    private static readonly object[] _input = [
        new object[] { true, _requestValid },
        new object[] { true, new CurrencyRequest(_requestValid) { From = "EUR" } }, // EUR is the reference currency and the rates API doesn't contain it by default
        new object[] { false, new CurrencyRequest(_requestValid) { To = "XXX" } }, // invalid To
        new object[] { false, new CurrencyRequest(_requestValid) { From = "XXX" } }, // invalid From
        new object[] { false, new CurrencyRequest(_requestValid) { From = "RON", To = "RON" } }, // valid values but the same
        new object[] { false, new CurrencyRequest(_requestValid) { From = "XXX", To = "XXX" } }, // invalid From and To but can be skipped because the same
        new object[] { false, new CurrencyRequest(_requestValid) { Money = [decimal.MinValue], From = "GBP" } }, // "underflow"
        new object[] { false, new CurrencyRequest(_requestValid) { Money = [decimal.MaxValue], From = "EUR", To = "IDR" } }, // overflow
    ];
    // clang-format on

    [TestCaseSource(nameof(_input))]
    public async Task TestConvert(bool valid, CurrencyRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _service.Convert(request);

                             // the money current currency is EUR and we need to convert it to RON and next USD
                             var money = request.Money.Select(m => m / UnitTestEndpoints.Rates[request.From] *
                                                                   UnitTestEndpoints.Rates[request.To]);
                             Assert.That(money.SequenceEqual(response.Money));
                         });

    [Test]
    public async Task TestFromTo() => await Try(
        true, async () =>
              {
                  var response = await _service.FromTo();

                  Assert.Multiple(() =>
                                  {
                                      Assert.That(UnitTestEndpoints.Rates.Keys.SequenceEqual(response.FromTo));
                                      Assert.That(response.DefaultFrom, Is.EqualTo("USD"));
                                      Assert.That(response.DefaultTo, Is.EqualTo("EUR"));
                                  });
              });
}
}
