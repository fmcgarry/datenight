using DateNight.App.Clients.DateNightApi;
using DateNight.App.Components.IdeaComponent;
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
    private int _selectedIndex = 0;
    private int _startIndex = 0;

    [Inject]
    public required IDateNightApiClient DateNightApiClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetTopIdeasInRange(0, NumResults);
    }

    private async Task CreateNewIdeaBasedOnStolenIdea(IdeaModel selectedIdea)
    {
        await DateNightApiClient.CreateIdeaAsync(selectedIdea);
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
    }

    private async Task OnNextButtonClickedAsync(MouseEventArgs e)
    {
        _startIndex = _endIndex;
        _endIndex = _startIndex + NumResults;

        await GetTopIdeasInRange(_startIndex, _endIndex);

        _isNextButtonDisabled = _suggested.Count < NumResults;
        _isPreviousButtonDisabled = _startIndex < 1;
    }

    private async Task OnPreviousButtonClickAsync(MouseEventArgs e)
    {
        _startIndex -= NumResults;
        _endIndex = _startIndex + NumResults;

        await GetTopIdeasInRange(_startIndex, _endIndex);

        _isNextButtonDisabled = _suggested.Count < NumResults;
        _isPreviousButtonDisabled = _startIndex < 1;
    }

    private async Task OnStealButtonClickAsync(MouseEventArgs e)
    {
        var selectedIdea = _suggested[_selectedIndex].Idea;

        await UpdateSelectedIdeaAsStolen(selectedIdea);
        await CreateNewIdeaBasedOnStolenIdea(selectedIdea);
    }

    private async Task UpdateSelectedIdeaAsStolen(IdeaModel selectedIdea)
    {
        selectedIdea.State = IdeaModel.IdeaState.Stolen;
        await DateNightApiClient.UpdateIdeaAsync(selectedIdea);
    }
}