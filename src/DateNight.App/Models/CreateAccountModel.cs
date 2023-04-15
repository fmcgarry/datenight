using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Models;

public class CreateAccountModel
{
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 8)]
    public string Password { get; set; }
}