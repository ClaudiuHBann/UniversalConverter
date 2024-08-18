using System.Reflection;

using Shared.Validators;
using Shared.Services.UC;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        serviceCollection.AddScoped(
            _ => configuration);
        serviceCollection.AddTransient<UCService>();

        serviceCollection.AddTransient<LinkValidator>();

        serviceCollection.AddSingleton<RankUCService>();
        serviceCollection.AddSingleton<CommonUCService>();
        serviceCollection.AddTransient<RadixUCService>();
        serviceCollection.AddTransient<LinkZipUCService>();
        serviceCollection.AddTransient<CurrencyUCService>();
        serviceCollection.AddTransient<TemperatureUCService>();

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
