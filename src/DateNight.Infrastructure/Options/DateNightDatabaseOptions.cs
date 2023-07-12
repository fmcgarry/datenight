using System.ComponentModel.DataAnnotations;

namespace DateNight.Infrastructure.Options;

public class DateNightDatabaseOptions
{
    [Required]
    public string DatabaseId { get; set; } = string.Empty;

    [Required]
    public string IdeasContainer { get; set; } = string.Empty;

    public string UsersContainer { get; set; } = string.Empty;
}