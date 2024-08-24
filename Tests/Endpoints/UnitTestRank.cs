using Tests.Utilities;

using Shared.Services.UC;

namespace Tests.Endpoints
{
[TestFixture]
internal class UnitTestRank : UnitTestBase
{
    private readonly RankUCService _service;

    public UnitTestRank()
    {
        _service = DI.GetService<RankUCService>()!;
    }

    [Test]
    public async Task Test() => await Try(true, () => Task.CompletedTask);
}
}
