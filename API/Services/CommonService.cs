using System.Reflection;

using Shared.Utilities;

namespace API.Services
{
public class CommonService
(IServiceProvider provider)
{
    private readonly Dictionary<string, List<string>> _fromToAll = [];
    private readonly SemaphoreSlim _ss = new(1);

    private List<Type> FindAllServiceTypes()
    {
        List<Type> services = [];
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (!type.IsSealed)
            {
                continue;
            }

            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
            {
                if (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(BaseService<, >))
                {
                    continue;
                }

                services.Add(type);
            }
        }

        return services;
    }

    private async Task<Dictionary<string, List<string>>> FindFromToAll()
    {
        try
        {
            await _ss.WaitAsync();
            if (_fromToAll.Count > 0)
            {
                return _fromToAll;
            }

            foreach (var serviceType in FindAllServiceTypes())
            {
                var service = ActivatorUtilities.CreateInstance(provider, serviceType);
                var fromTo = await service.Invoke<Task<List<string>>>("FromTo");
                _fromToAll.Add(serviceType.Name.Replace("Service", null), fromTo);
            }

            return _fromToAll;
        }
        finally
        {
            _ss.Release();
        }
    }

    public async Task<Dictionary<string, List<string>>> FromToAll() => await FindFromToAll();
}
}
