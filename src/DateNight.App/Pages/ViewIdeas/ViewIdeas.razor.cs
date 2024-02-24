using DateNightApp.Clients.DateNightApi;
using DateNightApp.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;

namespace DateNightApp.Pages.ViewIdeas;

public partial class ViewIdeas
{
    private List<IdeaModel> _ideas = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    protected async Task GetAllIdeas()
    {
        _ideas = (await DateNightApiClient.GetAllIdeasAsync()).ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllIdeas();
    }
}