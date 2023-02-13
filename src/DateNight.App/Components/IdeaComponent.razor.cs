using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Components;

public partial class IdeaComponent
{
    [Parameter]
    public IdeaModel Idea { get; set; }
}