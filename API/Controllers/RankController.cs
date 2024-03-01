using Microsoft.AspNetCore.Mvc;

using API.Services;

using Shared.Requests;

namespace API.Controllers
{
[ApiController]
[Route("[controller]")]
public class RankController
(RankService service) : BaseController
{
    [HttpGet(nameof(Converters))]
    public async Task<ActionResult> Converters([FromBody] RankRequest request) =>
        MakeOk(await service.Converters(request));
}
}
