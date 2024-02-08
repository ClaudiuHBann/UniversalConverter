using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;

namespace API.Services
{
public class CurrencyService : BaseService<CurrencyRequest, CurrencyResponse>
{
    private const string Source = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

    private static Dictionary<string, decimal> _rates = [];
    private static DateTime _ratesLastUpdate = DateTime.MinValue;

    public override async Task<List<string>> FromTo()
    {
        await UpdateRates();
        return _rates.Select(rate => rate.Key).ToList();
    }

    protected override async Task Validate(CurrencyRequest request)
    {
        var fromTo = await FromTo();

        if (!fromTo.Contains(request.From.ToUpper()))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.Contains(request.To.ToUpper()))
        {
            throw new FromToException(this, false);
        }
    }

    public override async Task<CurrencyResponse> Convert(CurrencyRequest request)
    {
        await Validate(request);

        await UpdateRates();
        try
        {
            return new(request.Money.Select(money => money / _rates[request.From] * _rates[request.To]).ToList());
        }
        catch (OverflowException)
        {
            throw new ValueException($"The value converted from {request.From} to {request.To} is too small/big!");
        }
    }

    private bool ShouldUpdateRates()
    {
        if ((DateTime.UtcNow - _ratesLastUpdate).Days < 1)
        {
            return false;
        }

        _ratesLastUpdate = DateTime.UtcNow;
        return true;
    }

    private async Task UpdateRates()
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
                     .ToDictionary();
    }
}
}
