using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.AspNetCore.Components;

namespace DateNight.App.Pages;

public partial class ChangePassword
{
    private readonly ChangePasswordModel _model = new();
    private bool _isBusy;

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    private async Task OnValidSubmit()
    {
        _isBusy = true;
        await DateNightApiClient.UpdateUserPasswordAsync(_model.NewPassword);
        _isBusy = false;

        NavigationManager.NavigateTo("/account");
    }
}
