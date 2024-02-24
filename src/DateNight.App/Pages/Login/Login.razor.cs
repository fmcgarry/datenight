using DateNightApp.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;

namespace DateNightApp.Pages.Login;

public partial class Login
{
    private readonly LoginModel _loginModel = new();

    private string _errorMessage = string.Empty;

    private bool _isSending;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private async Task OnValidSubmit()
    {
        _isSending = true;
        bool isSuccessfulLogin = await DateNightApiClient.LoginUserAsync(_loginModel.Email, _loginModel.Password);
        _isSending = false;

        if (isSuccessfulLogin)
        {
            NavigationManager.NavigateTo("/ideas/active");
        }
        else
        {
            _errorMessage = "Invalid username or password.";
        }
    }
}