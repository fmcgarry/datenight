using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Models;

internal class CreateAccountModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
