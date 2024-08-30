using System.Net;

using Microsoft.AspNetCore.Mvc;

using Shared.Responses;
using Shared.Exceptions;

namespace API.Controllers
{
[Controller]
public abstract class BaseController : ControllerBase
{
    protected OkObjectResult MakeOk(object data) => Ok(data);

    protected BadRequestObjectResult MakeBadRequest(string message) => BadRequest(new ErrorResponse() {
        Code = HttpStatusCode.BadRequest, Message = message, TypeException = BaseException.EType.Unknown
    });

    protected UnauthorizedObjectResult MakeUnauthorized(string message) => Unauthorized(new ErrorResponse() {
        Code = HttpStatusCode.Unauthorized, Message = message, TypeException = BaseException.EType.Unknown
    });

    protected NotFoundObjectResult MakeNotFound(string message) => NotFound(new ErrorResponse() {
        Code = HttpStatusCode.NotFound, Message = message, TypeException = BaseException.EType.Unknown
    });

    protected ObjectResult MakeInternalAPIError(string message) => new(new ErrorResponse() {
        Code = HttpStatusCode.InternalServerError, Message = message, TypeException = BaseException.EType.Unknown
    }) { StatusCode = StatusCodes.Status500InternalServerError };
}
}
