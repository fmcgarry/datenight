using DateNight.App.Clients.DateNightApi;
using DateNight.App.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.ActiveDate;

public partial class ActiveDate
{
    private IdeaModel? _idea;
    private bool _isActiveIdeaAvailable;
    private bool _isLoading = true;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager Navigation { get; init; }

    protected override async Task OnInitializedAsync()
    {
        await GetActiveIdea();
    }

    private async Task GetActiveIdea()
    {
        _isLoading = true;

        _idea = await DateNightApiClient.GetActiveIdeaAsync();
        _isActiveIdeaAvailable = _idea is not null;

        _isLoading = false;
    }

    private async Task OnClickAbandonButton(MouseEventArgs e)
    {
        if (_idea is not null)
        {
            _idea.State = IdeaModel.IdeaState.Abandoned;
            await DateNightApiClient.UpdateIdeaAsync(_idea);
        }

        await GetActiveIdea();
    }

    private async Task OnClickCompleteIdeaButton(MouseEventArgs e)
    {
        if (_idea is not null)
        {
            _idea.State = IdeaModel.IdeaState.Completed;
            await DateNightApiClient.UpdateIdeaAsync(_idea);
        }

        await GetActiveIdea();
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