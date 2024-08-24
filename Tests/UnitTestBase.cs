using Shared.Exceptions;

namespace Tests
{
internal class UnitTestBase
{
    protected static async Task Try(bool valid, Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (BaseException exception)
        {
            Assert.That(valid, Is.False, exception.Message);
        }
        catch (Exception exception)
        {
            Assert.Fail(exception.Message);
        }
    }
}
}
