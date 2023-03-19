using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class EditIdeaModal
{
    private readonly string _editModalDescriptionId = "m" + Guid.NewGuid().ToString("N");
    private readonly string _editModalLabel = "m" + Guid.NewGuid().ToString("N");
    private readonly string _editModalTitleId = "m" + Guid.NewGuid().ToString("N");
    private IdeaModel _editIdea = new();

    [Parameter]
    public string Id { get; set; }

    [Parameter]
    public IdeaModel Idea { get; set; }

    [Parameter]
    public EventCallback OnEditFinishedCallback { get; set; }

    [Inject]
    internal IDateNightApiClient DateNightApiClient { get; set; }

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