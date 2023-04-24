using DateNight.App.Components.IdeaComponent;

namespace DateNight.App.Pages.Suggested;

internal class SuggestedModel
{
    public IdeaModel Idea { get; set; } = new();
    public int Rank { get; set; }
}