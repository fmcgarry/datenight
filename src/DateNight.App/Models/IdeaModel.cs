using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Models;

internal class IdeaModel
{
    public DateTime CreatedOn { get; set; }

    public string Description { get; set; }

    [Required]
    public string Title { get; set; }
}