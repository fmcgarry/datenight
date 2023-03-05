using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class ViewIdeas
{
    private List<IdeaModel> _ideas;

    [Inject]
    IDateNightApiClient DateNightApiClient { get; set; }

    protected async Task GetAllIdeas()
    {
        _ideas = (await DateNightApiClient.GetAllIdeasAsync()).ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllIdeas();
    }
}