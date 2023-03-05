using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class IdeaComponent
{
    private readonly string _editModalId = "m" + Guid.NewGuid().ToString("N");

    [Parameter]
    public IdeaModel Idea { get; set; }

    [Parameter]
    public EventCallback OnDeleteCallback { get; set; }

    [Inject]
    internal IDateNightApiClient DateNightApiClient { get; set; }

    private async Task Delete()
    {
        await DateNightApiClient.DeleteIdeaAsync(Idea);
        await OnDeleteCallback.InvokeAsync();
    }

    private async Task RefreshIdea()
    {
        Idea = await DateNightApiClient.GetIdeaAsync(Idea.Id);
    }
}