using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class Account
{
    private AccountModel _account = new();
    private bool _isDirty;
    private bool _isBusy;
    private bool _isLoading = true;

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAccountInfo();
        _isLoading = false;
    }

    private async Task GetAccountInfo()
    {
        var user = await DateNightApiClient.GetCurrentUserAsync();

        _account = new();

        if (user is not null)
        {
            _account.Name = user.Name;
            _account.Email = user.Email;
        }

        _isDirty = false;
    }

    private async Task OnValidSubmit()
    {
        _isBusy = true;
        await DateNightApiClient.UpdateUserAsync(_account.Name, _account.Email);
        _isBusy = false;

        await GetAccountInfo();
    }

    private void OnChangePasswordButtonClick(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/change-password");
    }
}