using Microsoft.AspNetCore.Mvc;

using Server.Services;
using Server.Requests;
using Server.Responses;

namespace Server.Controllers
{
[ApiController]
[Route("[controller]")]
public class CurrencyController
(CurrencyService service) : BaseController
{
    [HttpGet(nameof(FromTo))]
    public async Task<ActionResult> FromTo() => MakeOk(new FromToResponse(await service.FromTo()));

    [HttpPost(nameof(Convert))]
    public async Task<ActionResult> Convert([FromBody] CurrencyRequest request) =>
        MakeOk(await service.Convert(request));
}
}
