using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class IdeaComponent
{
    private readonly string _editModalId = "m" + Guid.NewGuid().ToString("N");

    [Parameter]
    public IdeaModel Idea { get; set; }
}