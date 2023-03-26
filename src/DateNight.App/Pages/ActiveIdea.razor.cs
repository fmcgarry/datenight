using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class ActiveIdea
{
    private IdeaModel? _idea;
    private bool _isActiveIdeaAvailable;
    private bool _isFirstLoad = true;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager Navigation { get; init; }

    protected override async Task OnInitializedAsync()
    {
        await GetActiveIdea();
        _isFirstLoad = false;
    }

    private async Task GetActiveIdea()
    {
        _idea = await DateNightApiClient.GetActiveIdeaAsync();
        _isActiveIdeaAvailable = _idea is not null;
    }

    private async Task OnClickGetRandomIdeaButton(MouseEventArgs e)
    {
        Navigation.NavigateTo("/ideas/random");
    }

    private async Task OnClickAbandonButton(MouseEventArgs e)
    {
    }

    private async Task OnClickCompleteIdeaButton(MouseEventArgs e)
    {
    }
}