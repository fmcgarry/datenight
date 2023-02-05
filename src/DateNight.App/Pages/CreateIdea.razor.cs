using DateNight.App.Models;
using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class CreateIdea
{
    private IdeaModel _idea = new();

    [Inject]
    public IDateNightApiClient DateNightApiClient { get; set; }

    private async Task HandleValidSubmit()
    {
        Idea idea = new()
        {
            Title = _idea.Title,
            Description = _idea.Description,
            CreatedOn = DateTime.UtcNow.Date,
        };

        await DateNightApiClient.CreateIdeaAsync(idea);

        _idea = new IdeaModel();
    }
}