using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;

namespace DateNight.Core.Services;

public class IdeaService : IIdeaService
{
    private readonly IIdeaRepository _ideaRepository;
    private readonly IAppLogger<IdeaService> _logger;
    private readonly Random _random = new();
    private readonly IUserService _userService;

    public IdeaService(IAppLogger<IdeaService> logger, IIdeaRepository ideaRepository, IUserService userService)
    {
        _logger = logger;
        _ideaRepository = ideaRepository;
        _userService = userService;
    }

    public async Task ActivateIdea(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _logger.LogInformation("Setting idea '{Id}' as active", id);

        var selectedIdea = await GetIdeaByIdAsync(id);
        var ideas = await GetAllUserIdeasAsync(selectedIdea.CreatedBy, true);

        foreach (var activeIdea in ideas.Where(idea => idea.State == IdeaState.Active))
        {
            activeIdea.State = IdeaState.None;
            await _ideaRepository.UpdateAsync(activeIdea);
        }

        selectedIdea.State = IdeaState.Active;
        await _ideaRepository.UpdateAsync(selectedIdea);

        _logger.LogInformation("Idea {Id} set as active", selectedIdea.Id);
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

    public Task<IEnumerable<Idea>> GetAllUserIdeasAsync(string? userId, bool includePartnerIdeas)
    {
        ArgumentNullException.ThrowIfNull(userId);

        _logger.LogInformation("Getting all ideas for user '{userId}'.", userId);

        return GetAllUserIdeasInternalAsync(userId, includePartnerIdeas);
    }

    public Task<Idea> GetIdeaByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        _logger.LogInformation("Getting idea '{Id}'.", id);

        return GetIdeaByIdInternalAsync(id);
    }

    public async Task<Idea?> GetRandomUserIdeaAsync(string userId)
    {
        _logger.LogInformation("Getting random idea");

        var ideas = await GetAllUserIdeasInternalAsync(userId, true);
        var nonActiveIdeas = ideas.Where(idea => idea.State != IdeaState.Active);

        int numIdeas = nonActiveIdeas.Count();
        _logger.LogDebug("Total number of non-active ideas: {numIdeas}", numIdeas);

        if (numIdeas == 0)
        {
            _logger.LogDebug("No ideas were found", numIdeas);
            return null;
        }

        int randomIndex = numIdeas > 1 ? _random.Next(0, numIdeas) : 0;
        _logger.LogDebug("Random index chosen: {randomIndex}", randomIndex);

        Idea idea = nonActiveIdeas.ElementAt(randomIndex);
        _logger.LogInformation("Returning random idea '{Id}'", idea.Id!);

        return idea;
    }

    public async Task<IEnumerable<Idea>> GetTopIdeas(int start, int end)
    {
        if (start > end)
        {
            throw new ArgumentOutOfRangeException(nameof(start), "Start cannot be greater than end.");
        }

        if (end < start)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "End cannot be less than start.");
        }

        var ideas = await _ideaRepository.GetAllAsync();
        var sortedIdeas = ideas.OrderByDescending(idea => idea.PopularityScore).ToArray();

        if (start < 0)
        {
            start = 0;
        }
        else if (start > sortedIdeas.Length)
        {
            start = sortedIdeas.Length;
        }

        if (end > sortedIdeas.Length)
        {
            end = sortedIdeas.Length;
        }

        return sortedIdeas[start..end];
    }

    public async Task<Idea> GetUserActiveIdeaAsync(string? userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        _logger.LogInformation("Getting currently active idea");

        var ideas = await GetAllUserIdeasInternalAsync(userId, true);
        var currentlyActiveIdea = ideas.FirstOrDefault(idea => idea.State == IdeaState.Active);

        if (currentlyActiveIdea is null)
        {
            throw new ArgumentException("No idea currently marked as active");
        }

        return currentlyActiveIdea;
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

        if (idea.State == IdeaState.Active)
        {
            await ActivateIdea(idea.Id);
        }

        await _ideaRepository.UpdateAsync(idea);
    }

    private async Task AddIdeaInternalAsync(Idea idea)
    {
        await _ideaRepository.AddAsync(idea);
    }

    private async Task<IEnumerable<Idea>> GetAllUserIdeasInternalAsync(string userId, bool includePartnerIdeas)
    {
        var ideas = new List<Idea>();

        var userIdeas = await _ideaRepository.GetAllUserIdeasAsync(userId);
        ideas.AddRange(userIdeas);

        if (includePartnerIdeas)
        {
            var partners = await _userService.GetUserPartners(userId);

            foreach (var partner in partners)
            {
                var partnerIdeas = await _ideaRepository.GetAllUserIdeasAsync(partner);
                ideas.AddRange(partnerIdeas);
            }
        }

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