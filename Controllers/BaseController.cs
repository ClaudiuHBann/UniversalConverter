using System.Net;

using Microsoft.AspNetCore.Mvc;

using Server.Responses;
using Server.Exceptions;

namespace Server.Controllers
{
[Controller]
public abstract class BaseController : ControllerBase
{
    protected async Task<ActionResult> Try(Func<Task<BaseResponse>> func)
    {
        try
        {
            return MakeOk(await func());
        }
        catch (BaseException exception)
        {
            return MakeResponse(exception.Error);
        }
        catch (Exception exception)
        {
            return MakeResponse(new(HttpStatusCode.InternalServerError, exception.Message));
        }
    }

    private OkObjectResult MakeOk(object data) => Ok(data);

    private ObjectResult MakeResponse(ErrorResponse error)
    {
        return error.Code switch {
            HttpStatusCode.BadRequest => MakeBadRequest(error.Message),
            HttpStatusCode.Unauthorized => Unauthorized(error),
            HttpStatusCode.NotFound => NotFound(error),
            HttpStatusCode.InternalServerError =>
                new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError },
            _ => MakeBadRequest("IDK Brah..."),
        };
    }

    private BadRequestObjectResult MakeBadRequest(string message) =>
        BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, message));
}
}
