using Microsoft.AspNetCore.Mvc;

using API.Services;

namespace API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CommonController
(CommonService service) : BaseController
{
    [HttpGet(nameof(FromToAll))]
    public async Task<ActionResult> FromToAll() => MakeOk(await service.FromToAll());
}
}
