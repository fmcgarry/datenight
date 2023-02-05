using DateNight.Core.Interfaces;
using DateNight.Infrastructure.Clients.DateNightApi;
using DateNight.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DateNight.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddDateNightApiClient(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DateNightApiClientOptions>(config.GetSection(DateNightApiClientOptions.DateNightApiClient));
        services.AddHttpClient(DateNightApiClient.HttpClientName);

        return services;
    }

    public static IServiceCollection AddRequiredInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}