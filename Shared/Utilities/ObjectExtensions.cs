using System.Reflection;

namespace Shared.Utilities
{
public static class ObjectExtensions
{
    public static Result Invoke<Result>(this object obj, string methodName, object?[]? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(obj);
        if (string.IsNullOrWhiteSpace(methodName))
        {
            throw new ArgumentException($"Method name '{methodName}' is not valid!");
        }

        // (non)public non(static) member from hierarchy
        var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                           BindingFlags.FlattenHierarchy;

        var method = obj.GetType().GetMethod(methodName, bindingFlags) ??
                     throw new MissingMethodException(
                         $"Method '{methodName}' with type '{obj.GetType().FullName}' doesn't exist!");

        if (method.IsGenericMethod)
        {
            var parameterTypes = parameters != null ? parameters.Select(param => param!.GetType()).ToArray() : [];
            method = method.MakeGenericMethod(parameterTypes);
        }

        try
        {
            var result = method.Invoke(obj, parameters);
            if (typeof(Result).IsValueType)
            {
                ArgumentNullException.ThrowIfNull(result);
            }

            return (Result)result!;
        }
        catch (TargetInvocationException ex)
        {
            throw ex.InnerException ?? ex;
        }
    }

    public static bool Equal(this object obj1, object obj2) => obj1.ToJSON() == obj2.ToJSON();
}
}
