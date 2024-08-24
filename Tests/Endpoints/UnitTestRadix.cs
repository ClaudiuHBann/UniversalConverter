using Tests.Utilities;

using Shared.Requests;
using Shared.Services.UC;

namespace Tests.Endpoints
{
    [TestFixture]
internal class UnitTestRadix : UnitTestBase
{
    private readonly RadixUCService _service;

    public UnitTestRadix()
    {
        _service = DI.GetService<RadixUCService>()!;
    }

    // clang-format off
    private static readonly RadixRequest _requestValid = new()
    {
        From = "10",
        To = "2",
        Numbers = ["69", "420"]
    };
    
    private static readonly object[] _input = [
        new object[] { true, new List<string>() { "1000101", "110100100" }, _requestValid },
        new object[] { true, new List<string>() { "1" }, new RadixRequest(_requestValid) { Numbers = ["0001"], From = "8" } }, // it will strip the 000 prefix
        new object[] { true, new List<string>() { "0" }, new RadixRequest(_requestValid) { Numbers = ["00"] } }, // it will strip the 0 prefix
        new object[] { true, new List<string>() { new('1', 64) }, new RadixRequest(_requestValid) { Numbers = [new('f', 16)], From = "16" } }, // 0xF x18 is the max for base 16 and uses a different branch to check for overflow
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { To = "XXX" } }, // invalid To
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { From = "XXX" } }, // invalid From
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { From = "10", To = "10" } }, // valid values but the same
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { From = "XXX", To = "XXX" } }, // invalid From and To but can be skipped because the same
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { Numbers = ["69.420"] } }, // only 0-9 and A-Z characters are valid
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { Numbers = ["0xDEADBEEF"] } }, // we use characters that are not valid for base 10
        new object[] { false, new List<string>(), new RadixRequest(_requestValid) { Numbers = [new('f', 18)], From = "16" } }, // 0xF x18 will overflow ulong when converting from base 16 to base 10
    ];
    // clang-format on

    [TestCaseSource(nameof(_input))]
    public async Task TestConvert(bool valid, List<string> result, RadixRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _service.Convert(request);
                             Assert.That(result.SequenceEqual(response.Numbers));
                         });

    [Test]
    public async Task TestFromTo() => await Try(true, async () =>
                                                      {
                                                          var response = await _service.FromTo();
                                                          Assert.Multiple(
                                                              () =>
                                                              {
                                                                  // bases start from 2 and go up to 36 (0-9 and A-Z
                                                                  // which is 10 + 26)
                                                                  Assert.That(Enumerable.Range(2, 35)
                                                                                  .Select(number => number.ToString())
                                                                                  .SequenceEqual(response.FromTo));
                                                                  Assert.That(response.DefaultFrom, Is.EqualTo("10"));
                                                                  Assert.That(response.DefaultTo, Is.EqualTo("2"));
                                                              });
                                                      });
}
}
