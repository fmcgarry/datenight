using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IDateNightApiClient _dateNightApiClient;
    private readonly IAppLogger<IdeaService> _logger;

    public IdeaService(IAppLogger<IdeaService> logger, IDateNightApiClient dateNightApiClient)
    {
        _logger = logger;
        _dateNightApiClient = dateNightApiClient;
    }
}