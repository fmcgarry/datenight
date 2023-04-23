using DateNight.App.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components.IdeaComponent;

public partial class IdeaComponent
{
    private readonly string _editModalId = "m" + Guid.NewGuid().ToString("N");
    private string _subtitleText = string.Empty;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Parameter]
    public required IdeaModel Idea { get; set; }

    [Parameter]
    public required EventCallback OnDeleteCallback { get; init; }

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