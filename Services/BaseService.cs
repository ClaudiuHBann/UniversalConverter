using Server.Requests;
using Server.Responses;

namespace Server.Services
{
public abstract class BaseService<Request, Response>() : IService
    where Request : BaseRequest
    where Response : BaseResponse
{
    public abstract Task<List<string>> FromTo();

    protected virtual async Task Validate(Request request) => await Task.CompletedTask;
    public abstract Task<Response> Convert(Request request);
}
}
