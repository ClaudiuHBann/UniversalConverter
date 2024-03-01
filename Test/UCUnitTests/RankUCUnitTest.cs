using Shared.Services.UC;

namespace Test.UCUnitTests
{
[TestClass]
public class RankUCUnitTest : BaseUCUnitTest
{
    private readonly UCService _uc = new();

    // TODO: add tests for ranking

    [TestMethod]
    public async Task TestRank() => await Try(true, () => Task.CompletedTask);
}
}
