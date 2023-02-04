using DateNight.Infrastructure.Clients.DateNightApi;
using Microsoft.Extensions.DependencyInjection;

namespace DateNight.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddDateNightApiClient(this IServiceCollection services, DateNightApiClientOptions options)
    {
        throw new NotSupportedException();
    }
}