using System.ComponentModel.DataAnnotations;

namespace DateNight.Infrastructure.Clients.DateNightApi;

public class DateNightApiClientOptions
{
    public const string DateNightApiClient = "DateNightApiClient";

    [Required]
    public string BaseAddress { get; set; } = string.Empty;
}