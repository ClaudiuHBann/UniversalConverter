using Microsoft.AspNetCore.Mvc;

using API.Services;

using Shared.Responses;

namespace API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CommonController
(CommonService service) : BaseController
{
    [HttpGet(nameof(FromToAll))]
    public async Task<ActionResult> FromToAll() => MakeOk(new CommonResponse(await service.FromToAll()));
}
}
