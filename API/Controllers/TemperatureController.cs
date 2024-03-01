﻿using Microsoft.AspNetCore.Mvc;

using API.Services;

using Shared.Requests;

namespace API.Controllers
{
[ApiController]
[Route("[controller]")]
public class TemperatureController
(TemperatureService service) : BaseController
{
    [HttpGet(nameof(FromTo))]
    public async Task<ActionResult> FromTo() => MakeOk(await service.FromTo());

    [HttpPost(nameof(Convert))]
    public async Task<ActionResult> Convert([FromBody] TemperatureRequest request) =>
        MakeOk(await service.Convert(request));
}
}
