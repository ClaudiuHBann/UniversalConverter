namespace Shared.Utilities
{
public static class ObjectExtensions
{
    public static Result Invoke<Result>(this object obj, string methodName, object?[]? parameters = null)
    {
        var method = obj.GetType().GetMethod(methodName);
        var methodResult = method!.Invoke(obj, parameters)!;
        return (Result)methodResult;
    }
}
}
