using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IRepository<Idea> _ideaRepository;
    private readonly IAppLogger<IdeaService> _logger;

    public IdeaService(IAppLogger<IdeaService> logger, IRepository<Idea> ideaRepository)
    {
        _logger = logger;
        _ideaRepository = ideaRepository;
    }

    public Task AddIdeaAsync(Idea idea)
    {
        ArgumentNullException.ThrowIfNull(idea.Id);
        _logger.LogInformation("Adding idea '{Id}'.", idea.Id);

        return AddIdeaInternalAsync(idea);
    }

    private async Task AddIdeaInternalAsync(Idea idea)
    {
        await _ideaRepository.AddAsync(idea);
    }
}