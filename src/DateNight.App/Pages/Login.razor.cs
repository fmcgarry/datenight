using System.ComponentModel.DataAnnotations;

namespace DateNight.App.Pages
{
    public partial class Login
    {
        private class LoginModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        private LoginModel _loginModel;

        protected override void OnInitialized()
        {
            _loginModel = new LoginModel();
        }

        private void OnValidSubmit()
        {
        }
    }
}