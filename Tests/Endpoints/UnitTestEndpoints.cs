using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Tests.Endpoints
{
[SetUpFixture]
internal class UnitTestEndpoints
{
    private const string _source = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

    public static Dictionary<string, decimal> Rates { get; private set; }

    [OneTimeSetUp]
    public async Task SetUp()
    {
        Rates = await FindRates();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
    }

    private static async Task<Dictionary<string, decimal>> FindRates()
    {
        using HttpClient client = new();

        var xmlString = await client.GetStringAsync(_source);
        var xml = XDocument.Parse(xmlString);

        var nsManager = new XmlNamespaceManager(new NameTable());
        nsManager.AddNamespace("ecb", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        return xml.XPathSelectElements("//ecb:Cube[@currency and @rate]", nsManager)
            .Select(cube => (cube.Attribute("currency")!.Value, decimal.Parse(cube.Attribute("rate")!.Value)))
            .Append(("EUR", 1m))
            .ToDictionary();
    }
}
}
