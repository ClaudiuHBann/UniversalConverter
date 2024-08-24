using Tests.Utilities;

using Shared.Requests;
using Shared.Utilities;
using Shared.Services.UC;

namespace Tests.Endpoints
{
[TestFixture]
internal class UnitTestTemperature : UnitTestBase
{
    private readonly TemperatureUCService _service;

    public UnitTestTemperature()
    {
        _service = DI.GetService<TemperatureUCService>()!;
    }

    // clang-format off
    private static readonly TemperatureRequest _requestValid = new()
    {
        From = "Celsius",
        To = "Kelvin",
        Temperatures = [69.420, 420.69]
    };
    
    private static readonly object[] _input = [
        new object[] { true, new List<double>() { 342.57, 693.84 }, _requestValid },
        new object[] { true, new List<double>() { 156.956, 789.242 }, new TemperatureRequest(_requestValid) { To = "Fahrenheit" } },
        new object[] { true, new List<double>() { 616.626, 1248.912 }, new TemperatureRequest(_requestValid) { To = "Rankine" } },
        new object[] { true, new List<double>() { 20.789, 215.939 }, new TemperatureRequest(_requestValid) { From = "Fahrenheit", To = "Celsius" } },
        new object[] { true, new List<double>() { 293.939, 489.089 }, new TemperatureRequest(_requestValid) { From = "Fahrenheit", To = "Kelvin" } },
        new object[] { true, new List<double>() { 529.09, 880.36 }, new TemperatureRequest(_requestValid) { From = "Fahrenheit", To = "Rankine" } },
        new object[] { true, new List<double>() { -203.73, 147.54 }, new TemperatureRequest(_requestValid) { From = "Kelvin", To = "Celsius" } },
        new object[] { true, new List<double>() { -334.714, 297.572 }, new TemperatureRequest(_requestValid) { From = "Kelvin", To = "Fahrenheit" } },
        new object[] { true, new List<double>() { 124.956, 757.242 }, new TemperatureRequest(_requestValid) { From = "Kelvin", To = "Rankine" } },
        new object[] { true, new List<double>() { -234.583, -39.433 }, new TemperatureRequest(_requestValid) { From = "Rankine", To = "Celsius" } },
        new object[] { true, new List<double>() { -390.25, -38.98 }, new TemperatureRequest(_requestValid) { From = "Rankine", To = "Fahrenheit" } },
        new object[] { true, new List<double>() { 38.567, 233.717 }, new TemperatureRequest(_requestValid) { From = "Rankine", To = "Kelvin" } },
        new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { To = "XXX" } }, // invalid To
        new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { From = "XXX" } }, // invalid From
        new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { From = "Celsius", To = "Celsius" } }, // valid values but the same
        new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { From = "XXX", To = "XXX" } }, // invalid From and To but can be skipped because the same
     // new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { From = "Celsius", To = "Kelvin", Temperatures = [double.MaxValue] } }, // overflow
     // new object[] { false, new List<double>(), new TemperatureRequest(_requestValid) { From = "Kelvin", To = "Celsius", Temperatures = [double.MinValue] } }, // "underflow"
    ];
    // clang-format on

    [TestCaseSource(nameof(_input))]
    public async Task TestConvert(bool valid, List<double> result, TemperatureRequest request) =>
        await Try(valid, async () =>
                         {
                             var response = await _service.Convert(request);
                             Assert.That(response.Temperatures.SequenceEqual(result, new EqualityComparerDoubles()));
                         });

    [Test]
    public async Task TestFromTo() => await Try(
        true, async () =>
              {
                  var response = await _service.FromTo();
                  Assert.Multiple(
                      () =>
                      {
                          Assert.That(response.FromTo.SequenceEqual(["Celsius", "Fahrenheit", "Kelvin", "Rankine"]));
                          Assert.That(response.DefaultFrom, Is.EqualTo("Celsius"));
                          Assert.That(response.DefaultTo, Is.EqualTo("Fahrenheit"));
                      });
              });
}
}
