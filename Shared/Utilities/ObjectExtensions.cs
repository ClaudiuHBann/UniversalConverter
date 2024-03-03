using System.Reflection;

namespace Shared.Utilities
{
public static class ObjectExtensions
{
    public static Result Invoke<Result>(this object obj, string methodName, object?[]? parameters = null)
    {
        // TODO: refactor this because is very sensitive

        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                           BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod;
        var method = obj.GetType().GetMethod(methodName, bindingFlags);
        if (method!.IsGenericMethod)
        {
            var parametersTypes = parameters != null ? parameters.Select(param => param!.GetType()).ToArray() : [];
            method = method.MakeGenericMethod(parametersTypes);
        }

        var methodResult = method.Invoke(obj, parameters)!;
        return (Result)methodResult;
    }

    public static bool Equal(this object obj1, object obj2) => obj1.ToJSON() == obj2.ToJSON();
}
}
