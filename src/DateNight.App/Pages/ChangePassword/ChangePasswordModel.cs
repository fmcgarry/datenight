using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Pages.ChangePassword;

internal class ChangePasswordModel
{
    [Required(ErrorMessage = "Please enter a new password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm new password")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match")]
    public string NewPasswordConfirmation { get; set; } = string.Empty;
}