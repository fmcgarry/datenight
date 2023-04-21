using DateNight.App.Models;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages;

public partial class Account
{
    private readonly AccountModel _account = new();
    private bool _isDirty = false;

    private void OnValidSubmit()
    {
        _isDirty = false;
    }

    private void OnChangePasswordButtonClick(MouseEventArgs e)
    {

    }
}