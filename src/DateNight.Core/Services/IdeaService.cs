using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IAppLogger<IdeaService> _logger;

    public IdeaService(IAppLogger<IdeaService> logger)
    {
        _logger = logger;
    }

    public async Task AddIdeaAsync(Idea idea)
    {
        throw new NotImplementedException();
    }
}