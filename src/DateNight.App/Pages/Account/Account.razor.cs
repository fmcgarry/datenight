using DateNight.App.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.Account;

public partial class Account
{
    private readonly PartnerCodeModel _partnerCode = new();
    private readonly List<PartnerModel> _partners = new();
    private AccountModel _account = new();
    private bool _isBusy;
    private bool _isDirty;
    private bool _isInPartnership;
    private bool _isLoading = true;
    private string _userPartnerCode = string.Empty;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    protected override async Task OnInitializedAsync()
    {
        await GetAccountInfo();
    }

    private async Task GetAccountInfo()
    {
        _isLoading = true;
        var user = await DateNightApiClient.GetCurrentUserAsync();

        _account = new AccountModel();

        if (user is not null)
        {
            _account.Name = user.Name;
            _account.Email = user.Email;
            _userPartnerCode = user.Id[..8];

            await UpdatePartners(user.Partners);
        }

        _isDirty = false;
        _isLoading = false;
    }

    private async Task OnAccountFormValidSubmit()
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

    private async Task OnLeaveButtonClick(MouseEventArgs e)
    {
        foreach (var partner in _partners)
        {
            await DateNightApiClient.RemovePartner(partner.Id);
        }

        await GetAccountInfo();
    }

    private async Task OnPartnerFormValidSubmit()
    {
        await DateNightApiClient.AddPartner(_partnerCode.Code);
        await GetAccountInfo();
    }

    private async Task UpdatePartners(IEnumerable<string> partnerIds)
    {
        _isInPartnership = partnerIds.Any();

        if (_isInPartnership)
        {
            _partners.Clear();

            foreach (var partnerId in partnerIds)
            {
                var partner = await DateNightApiClient.GetUserAsync(partnerId);

                if (partner is not null)
                {
                    _partners.Add(new PartnerModel() { Id = partner.Id, Name = partner.Name });
                }
            }
        }
    }
}