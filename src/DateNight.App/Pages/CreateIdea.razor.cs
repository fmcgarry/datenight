using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class CreateIdea
{
    private IdeaModel _idea = new();

    [Inject]
    internal IDateNightApiClient DateNightApiClient { get; set; }

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