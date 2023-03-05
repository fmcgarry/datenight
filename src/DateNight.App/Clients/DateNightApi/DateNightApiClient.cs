using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DateNight.App.Clients.DateNightApi;

internal class DateNightApiClient : IDateNightApiClient
{
    public const string HttpClientBaseAddress = "https://localhost:7000/";
    public const string HttpClientName = "DateNightApiClient";

    private readonly HttpClient _httpClient;
    private readonly ILogger<DateNightApiClient> _logger;

    public DateNightApiClient(ILogger<DateNightApiClient> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;

        _httpClient = httpClientFactory.CreateClient(HttpClientName);
        _logger.LogInformation("HttpClient created with base address: {_httpClient.BaseAddress}", _httpClient.BaseAddress);
    }

    public async Task CreateIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Creating idea '{Title}'", idea.Title);

        var response = await _httpClient.PostAsJsonAsync("ideas", idea);
    }

    public async Task<IEnumerable<IdeaModel>> GetAllIdeasAsync()
    {
        _logger.LogInformation("Getting all ideas");

        var response = await _httpClient.GetAsync("ideas");
        var ideas = await response.Content.ReadFromJsonAsync<IEnumerable<IdeaModel>>();

        // CreatedOn is stored as UTC
        foreach (var idea in ideas)
        {
            idea.CreatedOn = idea.CreatedOn.ToLocalTime();
        }

        return ideas;
    }

    public async Task UpdateIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Updating idea '{Title}'", idea.Title);
    }
}