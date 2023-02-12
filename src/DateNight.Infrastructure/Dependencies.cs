using DateNight.Core.Interfaces;
using DateNight.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DateNight.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddRequiredInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}