using Shared.Requests;
using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class LinkZipUCUnitTest : BaseUCUnitTest
{
    private const string _urlRR = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
    private const string _codeRR = "1";

    private readonly UCService _uc = new();

    // clang-format off
    public static LinkZipRequest LinkZipRequestValid { get; } = new()
    {
        From = "Longifier",
        To = "Shortifier",
        URLs = [_urlRR]
    };

    public static IEnumerable<object[]> LinkZipInput { get; } = [
        [true, new List<string>() { _codeRR }, LinkZipRequestValid],
        [true, new List<string>() { _urlRR }, new LinkZipRequest(LinkZipRequestValid) { From = "Shortifier", To = "Longifier", URLs = [_codeRR] }],
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { URLs = Enumerable.Range(0, 70).Select(i => i.ToString()).ToList() }], // more than the maximum number of links
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { URLs = ["https://www.ma-ta.e.pe.mine.7-cutite/?"] }], // invalid link
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { To = "XXX" }], // invalid To
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { From = "XXX" }], // invalid From
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { From = "Longifier", To = "Longifier" }], // valid values but the same
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { From = "XXX", To = "XXX" }], // invalid From and To but can be skipped because the same
        [false, new List<string>(), new LinkZipRequest(LinkZipRequestValid) { URLs = [new string('a', 2048)] }], // overflow
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
