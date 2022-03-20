using java.math;
using System.Runtime.CompilerServices;

namespace Server.UConverter {
    public class CTemperature : UConverterBase {
        public static readonly List<string> categoryTemperature = new() { "Celsius", "Fahrenheit", "Kelvin" };

        private static readonly BigDecimal bd1 = new(1.8);
        private static readonly BigDecimal bd2 = new(32.0);
        private static readonly BigDecimal bd3 = new(273.15);
        private static readonly BigDecimal bd4 = new BigDecimal(5.0).divide(new(9.0), 15, RoundingMode.DOWN);

        private readonly Dictionary<string, Func<BigDecimal, BigDecimal>> degreesConvertingFunctions = new()
        {
            { "01", degrees => degrees.multiply(bd1.add(bd2)) },
            { "02", degrees => degrees.add(bd3) },
            { "10", degrees => degrees.subtract(bd2).multiply(bd4) },
            { "12", degrees => degrees.subtract(bd2).multiply(bd4).add(bd3) },
            { "20", degrees => degrees.subtract(bd3) },
            { "21", degrees => degrees.subtract(bd3).multiply(bd1).add(bd2) }
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string Convert(string degrees, int from, int to) => degreesConvertingFunctions[from.ToString() + to.ToString()].Invoke(new(degrees)).setScale(15, RoundingMode.DOWN).stripTrailingZeros().ToString();
    }
}
