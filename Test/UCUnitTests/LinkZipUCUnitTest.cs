using Shared.Requests;
using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class LinkZipUCUnitTest : BaseUCUnitTest
{
    // TODO: add tests for LinkZipUC
    private readonly UCService _uc = new();

    // clang-format off
    public static LinkZipRequest LinkZipRequestValid { get; } = new()
    {
        From = "",
        To = "",
        URLs = []
    };

    public static IEnumerable<object[]> LinkZipInput { get; } = [
        [true, new List<string>() { "", "" }, LinkZipRequestValid],
    ];
    // clang-format on

    [DataTestMethod]
    [DynamicData(nameof(LinkZipInput))]
    public async Task TestLinkZipConvert(bool valid, List<string> result, LinkZipRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _uc.LinkZip.Convert(request);
                             Assert.IsTrue(response.URLs.SequenceEqual(result));
                         });

    [TestMethod]
    public async Task TestLinkZipFromTo() =>
        await Try(true, async () =>
                        {
                            var response = await _uc.LinkZip.FromTo();
                            Assert.IsTrue(response.FromTo.SequenceEqual(["Shortifier", "Longifier"]));
                        });
}
}
