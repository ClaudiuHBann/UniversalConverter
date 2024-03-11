using Shared.Requests;

using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class TemperatureUCUnitTest : BaseUCUnitTest
{
    private readonly UCService _uc = new();

    // clang-format off
    public static TemperatureRequest TemperatureRequestValid { get; } = new()
    {
        From = "Celsius",
        To = "Kelvin",
        Temperatures = [69.420, 420.69]
    };
    
    public static IEnumerable<object[]> TemperatureInput { get; } = [
        [true, new List<double>() { 342.57, 693.8399999999999 }, TemperatureRequestValid],
        [true, new List<double>() { -203.72999999999996, 147.54000000000002 }, new TemperatureRequest(TemperatureRequestValid) { From = "Kelvin", To = "Celsius" }],
        [true, new List<double>() { 156.95600000000002, 789.242 }, new TemperatureRequest(TemperatureRequestValid) { To = "Fahrenheit" }],
        [true, new List<double>() { 20.78888890552, 215.93888906163997 }, new TemperatureRequest(TemperatureRequestValid) { From = "Fahrenheit", To = "Celsius" }],
        [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { To = "XXX" }], // invalid To
        [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { From = "XXX" }], // invalid From
        [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { From = "Celsius", To = "Celsius" }], // valid values but the same
        [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { From = "XXX", To = "XXX" }], // invalid From and To but can be skipped because the same
     // [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { From = "Celsius", To = "Kelvin", Temperatures = [double.MaxValue] }], // overflow
     // [false, new List<double>(), new TemperatureRequest(TemperatureRequestValid) { From = "Kelvin", To = "Celsius", Temperatures = [double.MinValue] }], // "underflow"
    ];
    // clang-format on

    [DataTestMethod]
    [DynamicData(nameof(TemperatureInput))]
    public async Task TestTemperatureConvert(bool valid, List<double> result, TemperatureRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _uc.Temperature.Convert(request);
                             Assert.IsTrue(response.Temperatures.SequenceEqual(result));
                         });

    [TestMethod]
    public async Task TestTemperatureFromTo() =>
        await Try(true, async () =>
                        {
                            var response = await _uc.Temperature.FromTo();
                            Assert.IsTrue(response.FromTo.SequenceEqual(["Celsius", "Fahrenheit", "Kelvin"]));
                            Assert.IsTrue(response.DefaultFrom == "Celsius");
                            Assert.IsTrue(response.DefaultTo == "Fahrenheit");
                        });
}
}
