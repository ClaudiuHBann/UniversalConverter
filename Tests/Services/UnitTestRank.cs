using API.Services;

using Tests.Utilities;

namespace Tests.Services
{
[TestFixture]
internal class UnitTestRank : UnitTestBase
{
    private readonly RankService _service;

    public UnitTestRank()
    {
        _service = DI.GetService<RankService>()!;
    }

    // TODO: Implement tests

    [Test]
    public async Task Test() => await Try(true, () => Task.CompletedTask);
}
}
