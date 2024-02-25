using System.Reflection;

namespace Shared.Utilities
{
public static class ObjectExtensions
{
    public static Result Invoke<Result>(this object obj, string methodName, object?[]? parameters = null)
    {
        var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic |
                                                             BindingFlags.Instance | BindingFlags.Static);
        var methodResult = method!.Invoke(obj, parameters)!;
        return (Result)methodResult;
    }
}
}
