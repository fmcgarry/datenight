using DateNightApp.Clients.DateNightApi;
using DateNightApp.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;

namespace DateNightApp.Pages.CreateIdea;

public partial class CreateIdea
{
    private IdeaModel _idea = new();
    private IdeaModel? _stolenIdea;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Parameter]
    public string? StolenId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (StolenId is not null)
        {
            _stolenIdea = await DateNightApiClient.GetIdeaAsync(StolenId);

            if (_stolenIdea is not null)
            {
                _idea.Title = _stolenIdea.Title;
            }
        }
    }

    private async Task HandleValidSubmit()
    {
        IdeaModel idea = new()
        {
            Title = _idea.Title,
            Description = _idea.Description,
            CreatedOn = DateTime.UtcNow.Date,
        };

        if (_stolenIdea is not null)
        {
            _stolenIdea.State = IdeaModel.IdeaState.Stolen;
            await DateNightApiClient.UpdateIdeaAsync(_stolenIdea);
        }

        await DateNightApiClient.CreateIdeaAsync(idea);

        _idea = new IdeaModel();
    }
}