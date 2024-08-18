using API.Services;

using Tests.Utilities;

namespace Tests.Services
{
[TestFixture]
internal class UnitTestCommon : UnitTestBase
{
    private readonly CommonService _serviceCommon;
    private readonly CurrencyService _serviceCurrency;
    private readonly LinkZipService _serviceLinkZip;
    private readonly RadixService _serviceRadix;
    private readonly TemperatureService _serviceTemperature;

    public UnitTestCommon()
    {
        _serviceCommon = DI.GetService<CommonService>()!;
        _serviceCurrency = DI.GetService<CurrencyService>()!;
        _serviceLinkZip = DI.GetService<LinkZipService>()!;
        _serviceRadix = DI.GetService<RadixService>()!;
        _serviceTemperature = DI.GetService<TemperatureService>()!;
    }

    private async Task<Dictionary<string, List<string>>> CreateFromToAll()
    {
        Dictionary<string, List<string>> fromToAll = [];

        fromToAll.Add("Currency", (await _serviceCurrency.FromTo()).FromTo);
        fromToAll.Add("LinkZip", (await _serviceLinkZip.FromTo()).FromTo);
        fromToAll.Add("Radix", (await _serviceRadix.FromTo()).FromTo);
        fromToAll.Add("Temperature", (await _serviceTemperature.FromTo()).FromTo);

        return fromToAll;
    }

    [Test]
    public async Task TestFromToAll() =>
        await Try(true, async () =>
                        {
                            var response = await _serviceCommon.FromToAll();
                            var fromToAll = await CreateFromToAll();

                            Assert.That(fromToAll.Keys.SequenceEqual(response.FromToAll.Keys));
                            foreach (var (key, value) in fromToAll)
                            {
                                Assert.That(value.SequenceEqual(response.FromToAll[key].FromTo));
                            }
                        });
}
}
