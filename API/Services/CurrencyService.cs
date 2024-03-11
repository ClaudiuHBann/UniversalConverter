using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

using API.Entities;

namespace API.Services
{
public class CurrencyService : BaseService<CurrencyRequest, CurrencyResponse>
{
    private const string Source = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

    // the rates doesnt need a mutex because the _ratesLastUpdate acts like it
    private static Dictionary<string, decimal> _rates = [];
    private static List<string> _fromTo = [];

    private const string _defaultFrom = "USD";
    private const string _defaultTo = "EUR";

    private static DateTime _ratesLastUpdate = DateTime.MinValue;
    private static readonly Mutex _mutexRatesLastUpdate = new();

    public CurrencyService(UCContext context) : base(context)
    {
    }

    public override bool IsConverter() => true;

    public override string GetServiceName() => "Currency";

    public override async Task<FromToResponse> FromTo()
    {
        await FindRates();
        return new(_fromTo, _defaultFrom, _defaultTo);
    }

    protected override async Task<CurrencyResponse> ConvertInternal(CurrencyRequest request)
    {
        await FindRates();
        try
        {
            // divide by from first because the currency is in EUR by default
            return new(request.Money.Select(money => money / _rates[request.From] * _rates[request.To]).ToList());
        }
        catch (OverflowException)
        {
            throw new ValueException($"The value converted from {request.From} to {request.To} is too small/big!");
        }
    }

    private static bool ShouldUpdateRates()
    {
        _mutexRatesLastUpdate.WaitOne();
        if ((DateTime.UtcNow - _ratesLastUpdate).Days < 1)
        {
            _mutexRatesLastUpdate.ReleaseMutex();
            return false;
        }

        _ratesLastUpdate = DateTime.UtcNow;

        _mutexRatesLastUpdate.ReleaseMutex();
        return true;
    }

    private static async Task FindRates()
    {
        if (!ShouldUpdateRates())
        {
            return;
        }

        using HttpClient client = new();

        var xmlString = await client.GetStringAsync(Source);
        var xml = XDocument.Parse(xmlString);

        var nsManager = new XmlNamespaceManager(new NameTable());
        nsManager.AddNamespace("ecb", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        _rates = xml.XPathSelectElements("//ecb:Cube[@currency and @rate]", nsManager)
                     .Select(cube => (cube.Attribute("currency")!.Value, decimal.Parse(cube.Attribute("rate")!.Value)))
                     .Append(("EUR", 1m))
                     .ToDictionary();
        _fromTo = _rates.Select(rate => rate.Key).ToList();
    }
}
}
