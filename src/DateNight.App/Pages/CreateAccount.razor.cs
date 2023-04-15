using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Pages;

public partial class CreateAccount
{
    private class CreateAccountModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }
    }

    private readonly CreateAccountModel _createAccountModel = new();

    private void OnValidSubmit()
    {
    }
}