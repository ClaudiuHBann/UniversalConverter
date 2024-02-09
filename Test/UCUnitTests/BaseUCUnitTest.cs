using Shared.Exceptions;

namespace Test.UCUnitTests
{
public class BaseUCUnitTest
{
    protected static async Task Try(bool valid, Action action)
    {
        try
        {
            await Task.Run(action);
        }
        catch (BaseException exception)
        {
            Assert.IsFalse(valid, exception.Message);
        }
        catch (Exception exception)
        {
            Assert.Fail(exception.Message);
        }
    }
}
}
