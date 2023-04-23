﻿using DateNight.App.Clients.DateNightApi;
using DateNight.App.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.ActiveDate;

public partial class ActiveDate
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

    private async Task OnClickAbandonButton(MouseEventArgs e)
    {
    }

    private async Task OnClickCompleteIdeaButton(MouseEventArgs e)
    {
    }

    private void OnClickCreateNewIdeaButton(MouseEventArgs e)
    {
        Navigation.NavigateTo("/ideas/create");
    }

    private void OnClickGetRandomIdeaButton(MouseEventArgs e)
    {
        Navigation.NavigateTo("/ideas/random");
    }
}