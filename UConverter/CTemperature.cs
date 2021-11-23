using System.Runtime.CompilerServices;

namespace Server.UConverter
{
    public class CTemperature
    {
        public static readonly List<string> categoryTemperature = new() { "Celsius", "Fahrenheit", "Kelvin" };

        private static readonly Dictionary<string, Func<double, double>> degreesConvertingFunctions = new()
        {
            { "01", degrees => degrees * 1.8 + 32.0 },
            { "02", degrees => degrees + 273.15 },
            { "10", degrees => (degrees - 32.0) * 5.0 / 9.0 },
            { "12", degrees => (degrees - 32.0) * 5.0 / 9.0 + 273.15 },
            { "20", degrees => degrees - 273.15 },
            { "21", degrees => (degrees - 273.15) * 1.8 + 32.0 }
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ConvertTemperature(string degrees, int from, int to) => degreesConvertingFunctions[from.ToString() + to.ToString()].Invoke(double.Parse(degrees)).ToString();
    }
}
