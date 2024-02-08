using Shared.Services;
using Shared.Requests;
using Shared.Responses;

namespace API.Services
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
