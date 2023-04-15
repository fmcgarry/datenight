using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Pages
{
    public partial class Login
    {
        private class LoginModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        private readonly LoginModel _loginModel = new();

        private void OnValidSubmit()
        {
        }
    }
}