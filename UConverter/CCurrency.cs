using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Server.UConverter
{
    public class CCurrency: UConverterBase
    {
        private Dictionary<string, double>? currenciesAndRates = null;
        public static readonly List<string> categoryCurrency = new() { "USD", "JPY", "BGN", "CZK", "DKK", "GBP", "HUF", "PLN", "RON", "SEK", "CHF", "ISK", "NOK", "HRK", "RUB", "TRY", "AUD", "BRL", "CAD", "CNY", "HKD", "IDR", "ILS", "INR", "KRW", "MXN", "MYR", "NZD", "PHP", "SGD", "THB", "ZAR" };

        private void GetCurrency()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new(stream);

            XmlDocument doc = new();
            doc.LoadXml(reader.ReadToEnd());

            currenciesAndRates = new Dictionary<string, double>();
            for (byte i = 2; i < doc.GetElementsByTagName("Cube").Count; i++)
            {
                currenciesAndRates.Add(doc.GetElementsByTagName("Cube")[i].Attributes[0].InnerText, double.Parse(doc.GetElementsByTagName("Cube")[i].Attributes[1].InnerText));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string Convert(string amount, int from, int to)
        {
            if (currenciesAndRates == null)
            {
                GetCurrency();
            }

            return (double.Parse(amount) / currenciesAndRates[categoryCurrency[from]] * currenciesAndRates[categoryCurrency[to]]).ToString();
        }
    }
}
