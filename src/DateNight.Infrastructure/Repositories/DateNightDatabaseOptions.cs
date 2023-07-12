using System.ComponentModel.DataAnnotations;

namespace DateNight.Infrastructure.Repositories;

public class DateNightDatabaseOptions
{
    [Required]
    public string DatabaseId { get; set; } = string.Empty;

    [Required]
    public string IdeasContainer { get; set; } = string.Empty;

    [Required]
    public string UsersContainer { get; set; } = string.Empty;
}