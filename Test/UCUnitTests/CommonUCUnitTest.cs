using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class CommonUCUnitTest : BaseUCUnitTest
{
    private readonly UCService _uc = new();
    // TODO: test me
    [TestMethod]
    public async Task TestCommonFromToAll() => await Try(true, async () =>
                                                               {
                                                                   var response = await _uc.Common.FromToAll();
                                                                   // Assert.IsTrue(response.FromToAll);
                                                               });
}
}
