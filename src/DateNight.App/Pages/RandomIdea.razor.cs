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
    public NavigationManager NavigationManager { get; set; }

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

        int loopCount = 1;

        do
        {
            // We don't want to display the same idea twice.
            // If we get the same idea 3 times in a row, it probably means there
            // is only 1 idea left in the collection.

            _idea = await DateNightApiClient.GetRandomIdeaAsync();

            loopCount++;
        } while (_idea.Id == _previousRandomIdeaId && loopCount < 3);

        _previousRandomIdeaId = _idea.Id;
    }

    private async Task OnDeletedIdea()
    {
        await GetRandomIdea();
    }

    private async Task SelectIdea()
    {
        await DateNightApiClient.SetIdeaAsActiveAsync(_idea);
        NavigationManager.NavigateTo("/ideas/active");
    }
}