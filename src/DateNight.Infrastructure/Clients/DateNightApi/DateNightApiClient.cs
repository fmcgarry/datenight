using DateNight.Core.Interfaces;

namespace DateNight.Infrastructure.Clients.DateNightApi;

public class DateNightApiClient : IDateNightApiClient
{
    private readonly IAppLogger<DateNightApiClient> _logger;

    public DateNightApiClient(IAppLogger<DateNightApiClient> logger)
    {
        _logger = logger;
    }
}