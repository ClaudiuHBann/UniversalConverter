namespace Shared.Services.UC
{
public class UCService : IService
{
    public RadixUCService Radix { get; set; } = new();
    public CurrencyUCService Currency { get; set; } = new();
    public TemperatureUCService Temperature { get; set; } = new();
    public LinkZipUCService LinkZip { get; set; } = new();
    public CommonUCService Common { get; set; } = new();
    public RankUCService Rank { get; set; } = new();
}
}
