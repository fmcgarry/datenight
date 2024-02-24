using DateNight.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DateNight.Infrastructure.Logging;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
