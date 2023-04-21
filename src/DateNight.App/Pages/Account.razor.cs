using DateNight.App.Models;

namespace DateNight.App.Pages;

public partial class Account
{
    private readonly AccountModel _account = new();
    private bool _isDirty = false;

    private void OnValidSubmit()
    {
        _isDirty = false;
    }
}