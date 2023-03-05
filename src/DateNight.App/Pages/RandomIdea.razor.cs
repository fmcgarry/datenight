using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class RandomIdea
{
    private IdeaModel _idea;
    private bool _isFirstLoad = true;
    private string _previousRandomIdeaId = string.Empty;

    [Inject]
    IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetRandomIdea();
        _isFirstLoad = false;
    }

    private async Task GetRandomIdea()
    {
        _idea = null;

        do
        {
            // We don't want to display the same idea twice.
            _idea = await DateNightApiClient.GetRandomIdeaAsync();
        } while (_idea.Id == _previousRandomIdeaId);

        _previousRandomIdeaId = _idea.Id;
    }

    private async Task OnDeletedIdea()
    {
        await GetRandomIdea();
    }

    private void SelectIdea()
    {
        //IdeaService.SelectIdea(_idea);
    }
}