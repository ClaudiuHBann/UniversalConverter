using Tests.Utilities;

using Shared.Utilities;
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

    private async Task<Dictionary<string, string>> CreateFromToAll()
    {
        Dictionary<string, string> fromToAll = [];

        fromToAll.Add("Currency", (await _service.Currency.FromTo()).ToJSON());
        fromToAll.Add("LinkZip", (await _service.LinkZip.FromTo()).ToJSON());
        fromToAll.Add("Radix", (await _service.Radix.FromTo()).ToJSON());
        fromToAll.Add("Temperature", (await _service.Temperature.FromTo()).ToJSON());

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
                                Assert.That(value, Is.EqualTo(response.FromToAll[key].ToJSON()));
                            }
                        });
}
}
