namespace Shared.Utilities
{
public class EqualityComparerDecimals
(byte precision = 5) : IEqualityComparer<decimal>
{
    private readonly decimal _epsilon = 1m / (decimal)Math.Pow(10, precision);

    public bool Equals(decimal x, decimal y) => Math.Abs(x - y) < _epsilon;

    public int GetHashCode(decimal obj) => obj.GetHashCode();
}

public class EqualityComparerDoubles
(byte precision = 3) : IEqualityComparer<double>
{
    private readonly double _epsilon = 1.0 / Math.Pow(10, precision);

    public bool Equals(double x, double y) => Math.Abs(x - y) < _epsilon;

    public int GetHashCode(double obj) => obj.GetHashCode();
}
}
