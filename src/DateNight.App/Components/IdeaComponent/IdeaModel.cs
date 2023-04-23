using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Components.IdeaComponent;

public class IdeaModel
{
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;
}