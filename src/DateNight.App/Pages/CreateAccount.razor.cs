using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class CreateAccount
{
    private readonly CreateAccountModel _createAccountModel = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private void OnValidSubmit()
    {
        // send the create user request
        DateNightApiClient.CreateUserAsync(_createAccountModel.Name, _createAccountModel.Email, _createAccountModel.Password);

        // log in using the email and password.
        DateNightApiClient.LoginUserAsync(_createAccountModel.Email, _createAccountModel.Password);

        // if login is successful, redirect to active idea page.
        NavigationManager.NavigateTo("/ideas/active");
    }
}