using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IRepository<Idea> _ideaRepository;
    private readonly IAppLogger<IdeaService> _logger;
    private readonly Random _random = new();

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

    public async Task DeleteIdeaAsync(Idea idea)
    {
        ArgumentNullException.ThrowIfNull(idea);
        ArgumentNullException.ThrowIfNull(idea.Id);

        _logger.LogInformation("Deleting idea '{Id}'.", idea.Id);

        await _ideaRepository.DeleteAsync(idea);
    }

    public async Task DeleteIdeaAsync(string id)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        var idea = await GetIdeaByIdAsync(id);

        await DeleteIdeaAsync(idea);
    }

    public Task<IEnumerable<Idea>> GetAllIdeasAsync()
    {
        _logger.LogInformation("Getting all ideas.");

        return GetAllIdeasInternalAsync();
    }

    public Task<Idea> GetIdeaByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _logger.LogInformation("Getting idea '{Id}'.", id);

        return GetIdeaByIdInternalAsync(id);
    }

    public async Task<Idea> GetRandomIdeaAsync()
    {
        _logger.LogInformation("Getting random idea");

        var ideas = await GetAllIdeasInternalAsync();
        var nonActiveIdeas = ideas.Where(idea => idea.State != IdeaState.Active);

        int numIdeas = nonActiveIdeas.Count();
        int randomIndex = numIdeas > 1 ? _random.Next(0, numIdeas) : 0;
        Idea idea = nonActiveIdeas.ElementAt(randomIndex);

        return idea;
    }

    public async Task SetActiveIdea(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _logger.LogInformation("Setting idea '{Id}' as active", id);

        var ideas = await GetAllIdeasInternalAsync();
        var currentActiveIdea = ideas.FirstOrDefault(idea => idea.State == IdeaState.Active);

        if (currentActiveIdea is not null)
        {
            currentActiveIdea.State = IdeaState.None;
        }
        else
        {
            _logger.LogInformation("No idea was currently set as active");
        }

        var idea = await GetIdeaByIdInternalAsync(id);
        idea.State = IdeaState.Active;

        _logger.LogInformation("Idea {Id} set as active", idea.Id);
    }

    public async Task UpdateIdeaAsync(Idea idea)
    {
        ArgumentNullException.ThrowIfNull(idea.Id);

        _logger.LogInformation("Updating idea '{Id}'.", idea.Id);

        bool isIdeaInRepository = (await _ideaRepository.GetByIdAsync(idea.Id)) is not null;

        if (!isIdeaInRepository)
        {
            throw new ArgumentException("Idea does not exist.", nameof(idea));
        }

        await _ideaRepository.UpdateAsync(idea);
    }

    private async Task AddIdeaInternalAsync(Idea idea)
    {
        await _ideaRepository.AddAsync(idea);
    }

    private async Task<IEnumerable<Idea>> GetAllIdeasInternalAsync()
    {
        var ideas = await _ideaRepository.GetAllAsync();
        return ideas;
    }

    private async Task<Idea> GetIdeaByIdInternalAsync(string id)
    {
        var idea = await _ideaRepository.GetByIdAsync(id);

        if (idea is null)
        {
            _logger.LogError("No idea found for id '{Id}'", id);
            throw new ArgumentOutOfRangeException(nameof(id), $"Idea '{id}' not found.");
        }

        return idea;
    }
}