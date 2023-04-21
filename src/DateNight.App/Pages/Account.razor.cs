using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class Account
{
    private readonly AccountModel _account = new();
    private bool _isDirty;
    private bool _isBusy;

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; set; }

    private async Task OnValidSubmit()
    {
        _isBusy = true;
        await DateNightApiClient.UpdateUserAsync(_account.Name, _account.Email);
        _isBusy = false;

        _isDirty = false;
    }

    private void OnChangePasswordButtonClick(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/change-password");
    }
}