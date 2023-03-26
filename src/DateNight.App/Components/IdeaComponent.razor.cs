using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class IdeaComponent
{
    private readonly string _editModalId = "m" + Guid.NewGuid().ToString("N");
    private string _subtitleText = string.Empty;

    [Parameter]
    public IdeaModel Idea { get; set; }

    [Parameter]
    public EventCallback OnDeleteCallback { get; set; }

    [Inject]
    internal IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ConstructSubtitleText();
    }

    private async Task ConstructSubtitleText()
    {
        var user = await DateNightApiClient.GetUserAsync(Idea.CreatedBy);
        _subtitleText = $"{user.Name}, {Idea.CreatedOn.ToShortDateString()}";
    }

    private async Task Delete()
    {
        await DateNightApiClient.DeleteIdeaAsync(Idea);
        await OnDeleteCallback.InvokeAsync();
    }

    private async Task RefreshIdea()
    {
        Idea = await DateNightApiClient.GetIdeaAsync(Idea.Id);
        await ConstructSubtitleText();
    }
}