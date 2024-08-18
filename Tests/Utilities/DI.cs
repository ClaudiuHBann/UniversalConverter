using API.Entities;
using API.Services;

using Shared.Validators;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace Tests.Utilities
{
internal static class DI
{
    private static readonly IConfiguration _configuration;
    private static readonly IServiceProvider _serviceProvider;

    static DI()
    {
        _configuration = CreateConfiguration();
        _serviceProvider = CreateServiceProvider(_configuration);
    }

    public static Service? GetService<Service>() => _serviceProvider.GetService<Service>();

    private static ServiceProvider CreateServiceProvider(IConfiguration configuration)
    {
        ServiceCollection serviceCollection = new();

        serviceCollection.AddLazyCache();

        serviceCollection.AddScoped(
            _ => configuration);
        serviceCollection.AddTransient<UCContext>();

        serviceCollection.AddTransient<LinkValidator>();

        serviceCollection.AddSingleton<RankService>();
        serviceCollection.AddSingleton<CommonService>();
        serviceCollection.AddTransient<RadixService>();
        serviceCollection.AddTransient<LinkZipService>();
        serviceCollection.AddTransient<CurrencyService>();
        serviceCollection.AddTransient<TemperatureService>();

        return serviceCollection.BuildServiceProvider();
    }

    private static IConfiguration CreateConfiguration()
    {
        ConfigurationBuilder builder = new();
        builder.AddUserSecrets(Assembly.GetExecutingAssembly());

        return builder.Build();
    }
}
}
