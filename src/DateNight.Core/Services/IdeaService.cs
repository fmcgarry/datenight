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

        idea.CreatedOn = DateTime.UtcNow;

        return AddIdeaInternalAsync(idea);
    }

    public async Task<IEnumerable<Idea>> GetAllIdeasAsync()
    {
        _logger.LogInformation("Getting all ideas.");
        var ideas = await _ideaRepository.GetAllAsync();

        return ideas;
    }

    public Task<Idea?> GetIdeaByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _logger.LogInformation("Getting idea '{Id}'.", id);

        return GetIdeaByIdInternalAsync(id);
    }

    public Task UpdateIdeaAsync(Idea idea)
    {
        ArgumentNullException.ThrowIfNull(idea.Id);
        _logger.LogInformation("Updating idea '{Id}'.", idea.Id);

        return UpdateIdeaInternalAsync(idea);
    }

    private async Task AddIdeaInternalAsync(Idea idea)
    {
        await _ideaRepository.AddAsync(idea);
    }

    private async Task<Idea?> GetIdeaByIdInternalAsync(string id)
    {
        var idea = await _ideaRepository.GetByIdAsync(id);

        return idea;
    }

    private async Task UpdateIdeaInternalAsync(Idea idea)
    {
        await _ideaRepository.UpdateAsync(idea);
    }
}