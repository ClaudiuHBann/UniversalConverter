using Server.Requests;
using Server.Responses;
using Server.Exceptions;

namespace Server.Services
{
public class TemperatureService : BaseService<TemperatureRequest, TemperatureResponse>
{
    private static readonly Dictionary<string, Func<double, double>> _temperatureDirectConversions =
        new() { { "Celsius->Celsius", temperature => temperature },
                { "Celsius->Fahrenheit", ToFahrenheit },
                { "Celsius->Kelvin", ToKelvin },
                { "Fahrenheit->Fahrenheit", temperature => temperature },
                { "Fahrenheit->Celsius", FromFahrenheit },
                { "Fahrenheit->Kelvin", temperature => ToKelvin(FromFahrenheit(temperature)) },
                { "Kelvin->Celsius", FromKelvin },
                { "Kelvin->Fahrenheit", temperature => ToFahrenheit(FromKelvin(temperature)) },
                { "Kelvin->Kelvin", temperature => temperature } };

    public override async Task<List<string>> FromTo() => await Task.FromResult(
        _temperatureDirectConversions.Select(tdc => tdc.Key.Split("->").First()).Distinct().ToList());

    protected override async Task Validate(TemperatureRequest request)
    {
        var fromTo = await FromTo();

        if (!fromTo.Contains(request.From.ToLower().ToUpperInvariant()))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.Contains(request.To.ToLower().ToUpperInvariant()))
        {
            throw new FromToException(this, false);
        }
    }

    public override async Task<TemperatureResponse> Convert(TemperatureRequest request)
    {
        var algorithm = FindDirectConversion(request);
        TemperatureResponse response = new(request.Temperatures.Select(temperature => algorithm(temperature)).ToList());
        return await Task.FromResult(response);
    }

    private Func<double, double> FindDirectConversion(TemperatureRequest request)
    {
        var algorithmName = $"{request.From.ToLower().ToUpperInvariant()}->{request.To.ToLower().ToUpperInvariant()}";
        return _temperatureDirectConversions[algorithmName];
    }

    private static double ToKelvin(double temperature) => temperature + 273.15;
    private static double FromKelvin(double temperature) => temperature - 273.15;
    private static double ToFahrenheit(double temperature) => temperature * 1.8 + 32.0;
    private static double FromFahrenheit(double temperature) => (temperature - 32.0) * 0.555555556;
}
}
