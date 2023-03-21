using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class ActiveIdea
{
    private IdeaModel _idea;

    [Inject]
    IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetActiveIdea();
    }

    private async Task GetActiveIdea()
    {
        _idea = await DateNightApiClient.GetActiveIdeaAsync();
    }

    private async Task OnClickAbandonButton(MouseEventArgs e)
    {
    }

    private async Task OnClickCompleteIdeaButton(MouseEventArgs e)
    {
    }
}