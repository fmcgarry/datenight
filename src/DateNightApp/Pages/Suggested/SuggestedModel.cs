using DateNightApp.Components.IdeaComponent;

namespace DateNightApp.Pages.Suggested;

internal class SuggestedModel
{
    public IdeaModel Idea { get; set; } = new();
    public int Rank { get; set; }
}