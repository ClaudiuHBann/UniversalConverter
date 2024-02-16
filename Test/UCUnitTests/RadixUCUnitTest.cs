using Shared.Requests;

using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class RadixUCUnitTest : BaseUCUnitTest
{
    private readonly UCService _uc = new();

    // clang-format off
    public static RadixRequest RadixRequestValid { get; } = new()
    {
        From = "10",
        To = "2",
        Numbers = ["69", "420"]
    };
    
    public static IEnumerable<object[]> RadixInput { get; } = [
        [true, new List<string>() { "1000101", "110100100" }, RadixRequestValid],
        [true, new List<string>() { "FACC" }, new RadixRequest(RadixRequestValid) { Numbers = ["0xFACC"], From = "16", To = "16" }], // it will strip the 0x prefix
        [true, new List<string>() { "10" }, new RadixRequest(RadixRequestValid) { Numbers = ["0b010"], From = "2" }], // it will strip the 0b0 prefix
        [true, new List<string>() { "1" }, new RadixRequest(RadixRequestValid) { Numbers = ["0001"], From = "8" }], // it will strip the 000 prefix
        [true, new List<string>() { "0" }, new RadixRequest(RadixRequestValid) { Numbers = ["00"] }], // it will strip the 0 prefix
        [true, new List<string>() { new('1', 64) }, new RadixRequest(RadixRequestValid) { Numbers = [new('f', 16)], From = "16" }], // 0xF x18 is the max for base 16 and uses a different branch to check for overflow
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { To = "XXX" }], // invalid To
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { From = "XXX" }], // invalid From
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { From = "10", To = "10" }], // valid values but the same
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { From = "XXX", To = "XXX" }], // invalid From and To but can be skipped because the same
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { Numbers = ["69.420"] }], // only 0-9 and A-Z characters are valid
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { Numbers = ["0xDEADBEEF"] }], // we use characters that are not valid for base 10
        [false, new List<string>(), new RadixRequest(RadixRequestValid) { Numbers = [new('f', 18)], From = "16" }], // 0xF x18 will overflow ulong when converting from base 16 to base 10
    ];
    // clang-format on

    [DataTestMethod]
    [DynamicData(nameof(RadixInput))]
    public async Task TestRadixConvert(bool valid, List<string> result, RadixRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _uc.Radix.Convert(request);
                             Assert.IsTrue(result.SequenceEqual(response.Numbers));
                         });

    [TestMethod]
    public async Task TestRadixFromTo() => await Try(
        true,
        async () =>
        {
            var response = await _uc.Radix.FromTo();
            // bases start from 2 and go up to 36 (0-9 and A-Z which is 10 + 26)
            Assert.IsTrue(Enumerable.Range(2, 35).Select(number => number.ToString()).SequenceEqual(response.FromTo));
        });
}
}
