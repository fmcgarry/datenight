using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class Account
{
    private readonly bool _isInPartnership = false;
    private readonly List<string> _partners = new() { "Jane", "Janet" };
    private AccountModel _account = new();
    private bool _isBusy;
    private bool _isDirty;
    private bool _isLoading = true;
    private string _partnerCode = string.Empty;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

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

    private void OnChangePasswordButtonClick(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/change-password");
    }

    private async Task OnValidSubmit()
    {
        _isBusy = true;
        await DateNightApiClient.UpdateUserAsync(_account.Name, _account.Email);
        _isBusy = false;

        await GetAccountInfo();
    }
}