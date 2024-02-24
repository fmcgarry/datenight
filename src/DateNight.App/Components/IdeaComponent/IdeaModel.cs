using System.ComponentModel.DataAnnotations;

namespace DateNightApp.Components.IdeaComponent;

public class IdeaModel
{
    public enum IdeaState
    {
        None,
        Active,
        Completed,
        Abandoned,
        Stolen
    }

    public int AbandondedCount { get; set; }
    public int CompletedCount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public IdeaState State { get; set; }
    public int StolenCount { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
}