using System.Reflection;

using API.Entities;

using Shared.Requests;
using Shared.Services;
using Shared.Responses;
using Shared.Utilities;

namespace API.Services
{
public class CommonService : BaseService<CommonRequest, CommonResponse>
{
    private readonly Dictionary<string, FromToResponse> _fromToAll = [];
    private readonly SemaphoreSlim _ssFromToAll = new(1);

    private readonly List<IService> _services = [];

    private readonly IServiceProvider _provider;

    public CommonService(IServiceProvider provider, UCContext context) : base(context)
    {
        _provider = provider;
    }

    public override bool IsConverter() => false;

    public override string GetServiceName() => "Common";

    public override Task<FromToResponse> FromTo() => throw new InvalidOperationException();

    protected override Task<CommonResponse> ConvertInternal(CommonRequest request) =>
        throw new InvalidOperationException();

    public async Task<CommonResponse> FromToAll() => new() { FromToAll = await FindFromToAll() };

    public List<IService> FindAllServices()
    {
        lock (_services)
        {
            if (_services.Count > 0)
            {
                return _services;
            }

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                {
                    if (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(BaseService<, >))
                    {
                        continue;
                    }

                    var service = ActivatorUtilities.CreateInstance(_provider, type);
                    if (!service.Invoke<bool>("IsConverter"))
                    {
                        continue;
                    }

                    _services.Add((IService)service);
                }
            }

            return _services;
        }
    }

    private async Task<Dictionary<string, FromToResponse>> FindFromToAll()
    {
        try
        {
            await _ssFromToAll.WaitAsync();
            if (_fromToAll.Count > 0)
            {
                return _fromToAll;
            }

            foreach (var service in FindAllServices())
            {
                var response = await service.Invoke<Task<FromToResponse>>("FromTo");
                _fromToAll.Add(service.GetType().Name.Replace("Service", null), response);
            }

            return _fromToAll;
        }
        finally
        {
            _ssFromToAll.Release();
        }
    }
}
}
