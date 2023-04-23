using DateNight.App.Clients.DateNightApi;
using DateNight.App.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class EditIdeaModal
{
    private readonly string _editModalDescriptionId = "m" + Guid.NewGuid().ToString("N");
    private readonly string _editModalLabel = "m" + Guid.NewGuid().ToString("N");
    private readonly string _editModalTitleId = "m" + Guid.NewGuid().ToString("N");
    private IdeaModel _editIdea = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Parameter]
    public required string Id { get; init; }

    [Parameter]
    public required IdeaModel Idea { get; init; }

    [Parameter]
    public required EventCallback OnEditFinishedCallback { get; init; }

    protected override void OnInitialized()
    {
        ResetEditIdea();
    }

    private void OnEditCancel()
    {
        ResetEditIdea();
    }

    private async Task OnEditValidSubmit()
    {
        await DateNightApiClient.UpdateIdeaAsync(Idea);

        ResetEditIdea();
        await OnEditFinishedCallback.InvokeAsync();
    }

    private void ResetEditIdea()
    {
        _editIdea = new()
        {
            Title = Idea.Title,
            Description = Idea.Description
        };
    }
}