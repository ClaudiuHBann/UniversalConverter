using Shared.Services.UC;

using Tests.Utilities;

namespace Tests.Services
{
[TestFixture]
internal class UnitTestRank : UnitTestBase
{
    private readonly RankUCService _service;

    public UnitTestRank()
    {
        _service = DI.GetService<RankUCService>()!;
    }

    // TODO: Implement tests

    [Test]
    public async Task Test() => await Try(true, () => Task.CompletedTask);
}
}
