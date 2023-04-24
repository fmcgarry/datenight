using DateNight.App.Clients.DateNightApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DateNight.App.Pages.Suggested;

public partial class Suggested
{
    private const int NumResults = 10;
    private readonly List<SuggestedModel> _suggested = new();
    private int _endIndex = NumResults;
    private bool _isNextButtonDisabled = false;
    private bool _isPreviousButtonDisabled = true;
    private SuggestedModel? _selected = null;
    private int _startIndex = 0;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; init; }

    [Inject]
    public required NavigationManager NavigationManager { get; init; }

    protected override async Task OnInitializedAsync()
    {
        _selected = null;
        await GetTopIdeasInRange(0, NumResults);
    }

    private async Task GetTopIdeasInRange(int start, int end)
    {
        var topIdeas = (await DateNightApiClient.GetTopIdeasAsync(start, end)).ToList();

        _suggested.Clear();

        for (int i = 0; i < topIdeas.Count; i++)
        {
            _suggested.Add(new SuggestedModel()
            {
                Rank = _startIndex + i + 1,
                Idea = topIdeas[i]
            });
        }

        _isNextButtonDisabled = _suggested.Count < NumResults;
        _isPreviousButtonDisabled = _startIndex < 1;
    }

    private async Task OnNextButtonClickedAsync(MouseEventArgs e)
    {
        _startIndex = _endIndex;
        _endIndex = _startIndex + NumResults;

        await GetTopIdeasInRange(_startIndex, _endIndex);
    }

    private async Task OnPreviousButtonClickAsync(MouseEventArgs e)
    {
        _startIndex -= NumResults;
        _endIndex = _startIndex + NumResults;

        await GetTopIdeasInRange(_startIndex, _endIndex);
    }

    private void OnStealButtonClick(MouseEventArgs e)
    {
        if (_selected is not null)
        {
            NavigationManager.NavigateTo($"ideas/create/{_selected.Idea.Id}");
        }
    }

    private void OnTableRowClick(SuggestedModel item)
    {
        _selected = item;
    }
}