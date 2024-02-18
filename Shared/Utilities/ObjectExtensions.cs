using System.Reflection;

namespace Shared.Utilities
{
public static class ObjectExtensions
{
    public static Result Invoke<Result>(this object obj, string methodName, object?[]? parameters = null)
    {
        var method = obj.GetType().GetMethod(
            methodName, BindingFlags.Default | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly |
                            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.CreateInstance |
                            BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty |
                            BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty |
                            BindingFlags.ExactBinding | BindingFlags.SuppressChangeType |
                            BindingFlags.OptionalParamBinding | BindingFlags.IgnoreReturn |
                            BindingFlags.DoNotWrapExceptions);
        var methodResult = method!.Invoke(obj, parameters)!;
        return (Result)methodResult;
    }
}
}
