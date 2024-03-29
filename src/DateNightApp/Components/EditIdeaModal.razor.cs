﻿using DateNightApp.Clients.DateNightApi;
using DateNightApp.Components.IdeaComponent;
using Microsoft.AspNetCore.Components;

namespace DateNightApp.Components;

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
        await DateNightApiClient.UpdateIdeaAsync(_editIdea);
        await OnEditFinishedCallback.InvokeAsync();
        ResetEditIdea();
    }

    private void ResetEditIdea()
    {
        _editIdea = new IdeaModel()
        {
            Id = Idea.Id,
            Title = Idea.Title,
            Description = Idea.Description,
            CreatedBy = Idea.CreatedBy,
            CreatedOn = Idea.CreatedOn
        };
    }
}