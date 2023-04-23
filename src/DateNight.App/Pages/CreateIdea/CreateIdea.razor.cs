using DateNight.App.Clients.DateNightApi;
using DateNight.App.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages.CreateIdea;

public partial class CreateIdea
{
    private IdeaModel _idea = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    private async Task HandleValidSubmit()
    {
        IdeaModel idea = new()
        {
            Title = _idea.Title,
            Description = _idea.Description,
            CreatedOn = DateTime.UtcNow.Date,
        };

        await DateNightApiClient.CreateIdeaAsync(idea);

        _idea = new IdeaModel();
    }
}