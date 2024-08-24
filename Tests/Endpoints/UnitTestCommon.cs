using Tests.Utilities;

using Shared.Services.UC;

namespace Tests.Endpoints
{
    [TestFixture]
internal class UnitTestCommon : UnitTestBase
{
    private readonly UCService _service;

    public UnitTestCommon()
    {
        _service = DI.GetService<UCService>()!;
    }

    private async Task<Dictionary<string, List<string>>> CreateFromToAll()
    {
        Dictionary<string, List<string>> fromToAll = [];

        fromToAll.Add("Currency", (await _service.Currency.FromTo()).FromTo);
        fromToAll.Add("LinkZip", (await _service.LinkZip.FromTo()).FromTo);
        fromToAll.Add("Radix", (await _service.Radix.FromTo()).FromTo);
        fromToAll.Add("Temperature", (await _service.Temperature.FromTo()).FromTo);

        return fromToAll;
    }

    [Test]
    public async Task TestFromToAll() =>
        await Try(true, async () =>
                        {
                            var response = await _service.Common.FromToAll();
                            var fromToAll = await CreateFromToAll();

                            Assert.That(fromToAll.Keys.SequenceEqual(response.FromToAll.Keys));
                            foreach (var (key, value) in fromToAll)
                            {
                                Assert.That(value.SequenceEqual(response.FromToAll[key].FromTo));
                            }
                        });
}
}
