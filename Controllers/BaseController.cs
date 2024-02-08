using System.Net;

using Microsoft.AspNetCore.Mvc;

using Server.Responses;

namespace Server.Controllers
{
[Controller]
public abstract class BaseController : ControllerBase
{
    protected OkObjectResult MakeOk(object data) => Ok(data);

    protected BadRequestObjectResult MakeBadRequest(string message) =>
        BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, message));

    protected UnauthorizedObjectResult MakeUnauthorized(string message) =>
        Unauthorized(new ErrorResponse(HttpStatusCode.Unauthorized, message));

    protected NotFoundObjectResult MakeNotFound(string message) => NotFound(new ErrorResponse(HttpStatusCode.NotFound,
                                                                                              message));

    protected ObjectResult MakeInternalServerError(string message) => new(new ErrorResponse(
        HttpStatusCode.InternalServerError, message)) { StatusCode = StatusCodes.Status500InternalServerError };
}
}
