using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class Login
{
    private readonly LoginModel _loginModel = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private async Task OnValidSubmit()
    {
        await DateNightApiClient.LoginUserAsync(_loginModel.Email, _loginModel.Password);
        NavigationManager.NavigateTo("/ideas/active");
    }
}