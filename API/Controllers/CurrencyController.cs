using Microsoft.AspNetCore.Mvc;

using API.Services;
using Shared.Requests;
using Shared.Responses;

namespace API.Controllers
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
