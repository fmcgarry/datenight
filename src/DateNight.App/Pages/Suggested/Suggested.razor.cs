using DateNight.App.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.Suggested;

public partial class Suggested
{
    private readonly List<SuggestedModel> _suggested = new();

    public Suggested()
    {
        for (int i = 0; i < 10; i++)
        {
            _suggested.Add(new SuggestedModel() { Rank = i + 1, Idea = new() { Title = $"Idea {i + 1}" } });
        }
    }

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; set; }

    private async Task OnNextButtonClickedAsync(MouseEventArgs e)
    {
    }

    private async Task OnPreviousButtonClickAsync(MouseEventArgs e)
    {
    }

    private async Task OnStealButtonClickAsync(MouseEventArgs e)
    {
    }
}