using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Shared.Requests;
using Shared.Exceptions;

using Test.Services.UC;

namespace Test
{
[TestClass]
public class UCUnitTest
{
    private const string Source = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

    private readonly UCService _uc = new();

    // clang-format off
    public static CurrencyRequest CurrencyRequestValid { get; } = new()
    {
        From = "RON",
        To = "USD",
        Money = [69.420m, 420.69m]
    };

    public static IEnumerable<object[]> CurrencyInput { get; } = [
        [true, CurrencyRequestValid],
        [false, new CurrencyRequest(CurrencyRequestValid) { From = "XXX" }],
        [false, new CurrencyRequest(CurrencyRequestValid) { To = "XXX" }],
        [false, new CurrencyRequest(CurrencyRequestValid) { From = "XXX", To = "XXX" }], // those values are not valid but they can be skipped because they are the same
        [false, new CurrencyRequest(CurrencyRequestValid) { Money = [decimal.MaxValue] }], // decimal.MaxValue from RON to USD will overflow
        [false, new CurrencyRequest(CurrencyRequestValid) { Money = [decimal.MinValue], From = "EUR", To = "GBP" }], // decimal.MinValue from USD to RON will "underflow"
    ];
    // clang-format on

    [DataTestMethod]
    [DynamicData(nameof(CurrencyInput))]
    public async Task TestCurrency(bool valid, CurrencyRequest request)
    {
        try
        {
            var rates = await FindRates();

            var responseFromTo = await _uc.Currency.FromTo();
            Assert.IsTrue(Enumerable.SequenceEqual(rates.Keys, responseFromTo.FromTo));

            var responseConvert = await _uc.Currency.Convert(request);
            // the money current currency is EUR and we need to convert it to RON and next USD
            var money = CurrencyRequestValid.Money.Select(m => m / rates[CurrencyRequestValid.From] *
                                                               rates[CurrencyRequestValid.To]);
            Assert.IsTrue(Enumerable.SequenceEqual(money, responseConvert.Money));
        }
        catch (BaseException exception)
        {
            Assert.IsFalse(valid, exception.Message);
        }
        catch (Exception exception)
        {
            Assert.Fail(exception.Message);
        }
    }

    private static async Task<Dictionary<string, decimal>> FindRates()
    {
        using HttpClient client = new();

        var xmlString = await client.GetStringAsync(Source);
        var xml = XDocument.Parse(xmlString);

        var nsManager = new XmlNamespaceManager(new NameTable());
        nsManager.AddNamespace("ecb", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        return xml.XPathSelectElements("//ecb:Cube[@currency and @rate]", nsManager)
            .Select(cube => (cube.Attribute("currency")!.Value, decimal.Parse(cube.Attribute("rate")!.Value)))
            .ToDictionary();
    }
}
}
