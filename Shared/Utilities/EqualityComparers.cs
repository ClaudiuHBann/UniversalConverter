namespace Shared.Utilities
{
public class EqualityComparerDoubles : IEqualityComparer<double>
{
    private const double _epsilon = 0.001D;

    public bool Equals(double x, double y) => Math.Abs(x - y) < _epsilon;

    public int GetHashCode(double obj) => obj.GetHashCode();
}
}
