using System.Net;

using Microsoft.AspNetCore.Mvc;

using Shared.Responses;

namespace API.Controllers
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

    protected ObjectResult MakeInternalAPIError(string message) => new(new ErrorResponse(
        HttpStatusCode.InternalServerError, message)) { StatusCode = StatusCodes.Status500InternalServerError };
}
}
