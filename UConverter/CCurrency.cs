using java.math;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Server.UConverter {
    public class CCurrency : UConverterBase {
        private Dictionary<string, double>? currenciesAndRates = null;
        public static readonly List<string> categoryCurrency = new() { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "ISK", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PLN", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR" };

        private void GetCurrency() {
            XmlDocument doc = new();
            doc.LoadXml(new HttpClient().GetStringAsync("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml").Result);

            currenciesAndRates = new Dictionary<string, double>();
            for (byte i = 2; i < doc.GetElementsByTagName("Cube").Count; i++) {
                var cubeElement = doc.GetElementsByTagName("Cube")[i];
                if (cubeElement != null && cubeElement.Attributes != null) {
                    currenciesAndRates.Add(cubeElement.Attributes[0].InnerText, double.Parse(cubeElement.Attributes[1].InnerText));
                }
            }
        }

        public override bool IsFormatted(Models.ConvertInfo ci) {
            if (ci.Items is null) {
                return false;
            }

            foreach (var item in ci.Items) {
                if (string.IsNullOrWhiteSpace(item) ||
                    item.StartsWith('.') || item.EndsWith('.')) {
                    return false;
                }

                foreach (var c in item) {
                    if (!char.IsDigit(c) && c != '.') {
                        return false;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string Convert(string amount, int from, int to) {
            if (currenciesAndRates == null) {
                GetCurrency();
            }

            if (currenciesAndRates != null) {
            return new BigDecimal(amount).divide(new(currenciesAndRates[categoryCurrency[from]]), 15, RoundingMode.DOWN).multiply(new(currenciesAndRates[categoryCurrency[to]])).setScale(15, RoundingMode.DOWN).stripTrailingZeros().ToString();
            }

            return "0.000000000000000";
        }
    }
}
