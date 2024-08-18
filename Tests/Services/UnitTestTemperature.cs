using API.Services;

using Shared.Requests;

using Tests.Utilities;

namespace Tests.Services
{
[TestFixture]
internal class UnitTestTemperature : UnitTestBase
{
    private readonly TemperatureService _service;

    public UnitTestTemperature()
    {
        _service = DI.GetService<TemperatureService>()!;
    }

    // clang-format off
    private static readonly TemperatureRequest _requestValid = new()
    {
        From = "Celsius",
        To = "Kelvin",
        Temperatures = [69.420, 420.69]
    };
    
    // TODO: add rankine tests

    private static readonly object[] _input = [
        new object[] { true, new List<double>() { 342.57, 693.8399999999999 }, _requestValid },
        new object[] { true, new List<double>() { -203.72999999999996, 147.54000000000002 }, new TemperatureRequest(_requestValid) { From = "Kelvin", To = "Celsius" } },
        new object[] { true, new List<double>() { 156.95600000000002, 789.242 }, new TemperatureRequest(_requestValid) { To = "Fahrenheit" } },
        new object[] { true, new List<double>() { 20.78888890552, 215.93888906163997 }, new TemperatureRequest(_requestValid) { From = "Fahrenheit", To = "Celsius" } },
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
                             Assert.That(response.Temperatures.SequenceEqual(result));
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
