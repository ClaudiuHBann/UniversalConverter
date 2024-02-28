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

    public static bool Equal(this object obj1, object obj2) => obj1.ToJSON() == obj2.ToJSON();
}
}
