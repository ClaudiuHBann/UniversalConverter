using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class CommonUCUnitTest : BaseUCUnitTest
{
    private readonly UCService _uc = new();

    private async Task<Dictionary<string, List<string>>> MakeFromToAll()
    {
        Dictionary<string, List<string>> fromToAll = [];

        fromToAll.Add("Currency", (await _uc.Currency.FromTo()).FromTo);
        fromToAll.Add("LinkZip", (await _uc.LinkZip.FromTo()).FromTo);
        fromToAll.Add("Radix", (await _uc.Radix.FromTo()).FromTo);
        fromToAll.Add("Temperature", (await _uc.Temperature.FromTo()).FromTo);

        return fromToAll;
    }

    [TestMethod]
    public async Task TestCommonFromToAll() =>
        await Try(true, async () =>
                        {
                            var response = await _uc.Common.FromToAll();
                            var fromToAll = await MakeFromToAll();

                            Assert.IsTrue(fromToAll.Keys.SequenceEqual(response.FromToAll.Keys));
                            foreach (var (key, value) in fromToAll)
                            {
                                Assert.IsTrue(value.SequenceEqual(response.FromToAll[key]));
                            }
                        });
}
}
