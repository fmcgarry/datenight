using System.ComponentModel.DataAnnotations;

namespace DateNight.Infrastructure.Options;

public class DateNightDatabaseOptions
{
    public const string DateNightDatabase = "DateNightDatabase";

    [Required]
    public string ContainerId { get; set; } = string.Empty;

    [Required]
    public string DatabaseId { get; set; } = string.Empty;
}