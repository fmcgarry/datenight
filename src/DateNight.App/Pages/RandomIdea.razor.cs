using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class RandomIdea
{
    private IdeaModel? _idea = null;
    private bool _isFirstLoad = true;
    private bool _isIdeaCollectionEmpty = false;
    private string _previousRandomIdeaId = string.Empty;

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    protected override async Task OnInitializedAsync()
    {
        await GetRandomIdea();
        _isFirstLoad = false;
    }

    private async Task GetRandomIdea()
    {
        _idea = null;
        _isIdeaCollectionEmpty = false;

        int loopCount = 1;

        do
        {
            // We don't want to display the same idea twice.
            // If we get the same idea 3 times in a row, it probably means there
            // is only 1 idea left in the collection.

            var idea = await DateNightApiClient.GetRandomIdeaAsync();

            if (idea is null)
            {
                _isIdeaCollectionEmpty = true;
                return;
            }

            _idea = idea;

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

    private void OnCreateMoreClick()
    {
        NavigationManager.NavigateTo("/ideas/create");
    }
}