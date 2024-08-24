using Tests.Utilities;

using Shared.Requests;
using Shared.Services.UC;

namespace Tests.Endpoints
{
[TestFixture]
internal class UnitTestLinkZip : UnitTestBase
{
    private const string _urlRR = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
    private const string _codeRR = "1";

    // TODO: Add a dynamic test case

    private readonly LinkZipUCService _service;

    public UnitTestLinkZip()
    {
        _service = DI.GetService<LinkZipUCService>()!;
    }

    // clang-format off
    private static readonly LinkZipRequest _requestValid = new()
    {
        From = "Longifier",
        To = "Shortifier",
        URLs = [_urlRR]
    };

    private static readonly object[] _input = [
        new object[] { true, new List<string>() { _codeRR }, _requestValid },
        new object[] { true, new List<string>() { _urlRR }, new LinkZipRequest(_requestValid) { From = "Shortifier", To = "Longifier", URLs = [_codeRR] } },
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { URLs = Enumerable.Range(0, 70).Select(i => i.ToString()).ToList() } }, // more than the maximum number of links
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { URLs = ["https://www.ma-ta.e.pe.mine.7-cutite/?"] } }, // invalid link
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { To = "XXX" } }, // invalid To
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { From = "XXX" } }, // invalid From
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { From = "Longifier", To = "Longifier" } }, // valid values but the same
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { From = "XXX", To = "XXX" } }, // invalid From and To but can be skipped because the same
        new object[] { false, new List<string>(), new LinkZipRequest(_requestValid) { URLs = [new string('a', 2048)] } }, // overflow
    ];
    // clang-format on

    [TestCaseSource(nameof(_input))]
    public async Task TestConvert(bool valid, List<string> result, LinkZipRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _service.Convert(request);
                             Assert.That(response.URLs.SequenceEqual(result));
                         });

    [Test]
    public async Task TestFromTo() =>
        await Try(true, async () =>
                        {
                            var response = await _service.FromTo();
                            Assert.Multiple(() =>
                                            {
                                                Assert.That(response.FromTo.SequenceEqual(["Shortifier", "Longifier"]));
                                                Assert.That(response.DefaultFrom, Is.EqualTo("Longifier"));
                                                Assert.That(response.DefaultTo, Is.EqualTo("Shortifier"));
                                            });
                        });
}
}
