using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;
using Shared.Utilities;

using API.Entities;

namespace API.Services
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

    public TemperatureService(UCContext context) : base(context)
    {
    }

    public override bool IsConverter() => true;

    public override async Task<List<string>> FromTo() => await Task.FromResult(
        _temperatureDirectConversions.Select(tdc => tdc.Key.Split("->").First()).Distinct().ToList());

    protected override async Task<TemperatureResponse> ConvertInternal(TemperatureRequest request)
    {
        var algorithm = FindDirectConversion(request);
        try
        {
            TemperatureResponse response =
                new(request.Temperatures.Select(temperature => algorithm(temperature)).ToList());
            return await Task.FromResult(response);
        }
        catch (OverflowException)
        {
            throw new ValueException($"The value converted from {request.From} to {request.To} is too small/big!");
        }
    }

    private static Func<double, double> FindDirectConversion(TemperatureRequest request)
    {
        var algorithmName = $"{request.From.ToLower().FirstCharToUpper()}->{request.To.ToLower().FirstCharToUpper()}";
        return _temperatureDirectConversions[algorithmName];
    }

    private static double ToKelvin(double temperature) => temperature + 273.15;
    private static double FromKelvin(double temperature) => temperature - 273.15;
    private static double ToFahrenheit(double temperature) => temperature * 1.8 + 32.0;
    private static double FromFahrenheit(double temperature) => (temperature - 32.0) * 0.555555556;
}
}
