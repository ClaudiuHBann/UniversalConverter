using Shared.Services;

namespace Test.Services.UC
{
public class UCService : IService
{
    public RadixUCService Radix { get; set; } = new();
    public CurrencyUCService Currency { get; set; } = new();
    public TemperatureUCService Temperature { get; set; } = new();
}
}
