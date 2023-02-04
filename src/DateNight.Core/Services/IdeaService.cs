using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IAppLogger<IdeaService> _logger;

    public IdeaService(IAppLogger<IdeaService> logger)
    {
        _logger = logger;
    }
}