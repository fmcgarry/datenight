using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class ChangePassword
{
    private readonly ChangePasswordModel _model = new();
    private bool _isBusy;

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    private async Task OnValidSubmit()
    {
        _isBusy = true;
        await Task.Factory.StartNew(() => Thread.Sleep(1000)); // TODO: replace with actual call
        _isBusy = false;
        NavigationManager.NavigateTo("/account");
    }
}
