using DateNight.App.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.ChangePassword;

public partial class ChangePassword
{
    private readonly ChangePasswordModel _model = new();
    private bool _isBusy;
    private bool _isDirty;

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

    private void OnExitButtonClick(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/account");
    }
}
