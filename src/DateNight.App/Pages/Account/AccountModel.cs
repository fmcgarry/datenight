using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Pages.Account;

public class AccountModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}