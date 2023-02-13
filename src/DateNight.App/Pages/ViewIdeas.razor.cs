using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class ViewIdeas
{
    private List<IdeaModel> _ideas;

    [Inject]
    IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _ideas = (await DateNightApiClient.GetAllIdeasAsync()).ToList();
    }

    private void Refresh()
    {
        OnInitialized();
    }
}