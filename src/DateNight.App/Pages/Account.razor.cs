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

    private void OnValidSubmit()
    {
        _isDirty = false;
    }

    private void OnChangePasswordButtonClick(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/change-password");
    }
}