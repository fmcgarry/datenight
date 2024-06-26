using DateNightApp.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;

namespace DateNightApp.Pages.CreateAccount;

public partial class CreateAccount
{
    private readonly CreateAccountModel _createAccountModel = new();

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    private async Task OnValidSubmit()
    {
        await DateNightApiClient.CreateUserAsync(_createAccountModel.Name, _createAccountModel.Email, _createAccountModel.Password);
        await DateNightApiClient.LoginUserAsync(_createAccountModel.Email, _createAccountModel.Password);

        NavigationManager.NavigateTo("/ideas/active");
    }
}