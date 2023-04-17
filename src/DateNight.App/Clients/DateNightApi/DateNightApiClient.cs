using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DateNight.App.Clients.DateNightApi;

internal class DateNightApiClient : IDateNightApiClient
{
    public const string HttpClientBaseAddress = "https://localhost:7000/";
    public const string HttpClientName = "DateNightApiClient";

    private const string _ideasEndpoint = "ideas";
    private const string _usersEndpoint = "users";

    private readonly HttpClient _httpClient;
    private readonly ILogger<DateNightApiClient> _logger;

    public DateNightApiClient(ILogger<DateNightApiClient> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;

        _httpClient = httpClientFactory.CreateClient(HttpClientName);
        _logger.LogInformation("HttpClient created with base address: {BaseAddress}", _httpClient.BaseAddress);
    }

    public async Task CreateIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Creating idea '{Title}'", idea.Title);

        var response = await _httpClient.PostAsJsonAsync(_ideasEndpoint, idea);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to create idea '{Id}'. Status code: {StatusCode}", idea.Id, response.StatusCode);
        }
    }

    public async Task CreateUserAsync(string name, string email, string password)
    {
        var content = JsonContent.Create(new
        {
            Email = email,
            Password = password,
            Name = name
        });

        var response = await _httpClient.PostAsync($@"{_usersEndpoint}\register", content);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to create user. Status code: {StatusCode}", response.StatusCode);
        }
    }

    public async Task DeleteIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Deleting idea '{Id}'", idea.Id);

        var response = await _httpClient.DeleteAsync($"{_ideasEndpoint}/{idea.Id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to delete idea '{Id}'. Status code: {StatusCode}", idea.Id, response.StatusCode);
        }
    }

    public async Task<IdeaModel?> GetActiveIdeaAsync()
    {
        _logger.LogInformation("Getting currently active idea");

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/active");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get the currently active idea. Status code: {StatusCode}", response.StatusCode);
            return null;
        }

        var idea = await response.Content.ReadFromJsonAsync<IdeaModel>();

        return idea;
    }

    public async Task<IEnumerable<IdeaModel>> GetAllIdeasAsync()
    {
        _logger.LogInformation("Getting all ideas");

        var response = await _httpClient.GetAsync(_ideasEndpoint);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get all ideas. Status code: {StatusCode}", response.StatusCode);
        }

        var ideas = await response.Content.ReadFromJsonAsync<IEnumerable<IdeaModel>>();

        // CreatedOn is stored as UTC
        foreach (var idea in ideas)
        {
            idea.CreatedOn = idea.CreatedOn.ToLocalTime();
        }

        return ideas;
    }

    public async Task<IdeaModel> GetIdeaAsync(string id)
    {
        _logger.LogInformation("Getting idea '{Id}'", id);

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get idea '{Id}'. Status code: {StatusCode}", id, response.StatusCode);
        }

        var idea = await response.Content.ReadFromJsonAsync<IdeaModel>();

        return idea;
    }

    public async Task<IdeaModel> GetRandomIdeaAsync()
    {
        _logger.LogInformation("Getting random idea");

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/random");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get a random idea. Status code: {StatusCode}", response.StatusCode);
        }

        var idea = await response.Content.ReadFromJsonAsync<IdeaModel>();

        return idea;
    }

    public async Task<UserModel> GetUserAsync(string id)
    {
        _logger.LogInformation("Getting user '{id}'", id);

        var response = await _httpClient.GetAsync($"{_usersEndpoint}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get user '{id}'. Status code: {StatusCode}", id, response.StatusCode);
        }

        var user = await response.Content.ReadFromJsonAsync<UserModel>();

        return user;
    }

    public async Task<bool> LoginUserAsync(string email, string password)
    {
        var content = JsonContent.Create(new
        {
            Email = email,
            Password = password
        });

        var response = await _httpClient.PostAsync($@"{_usersEndpoint}\login", content);

        if (response.IsSuccessStatusCode)
        {
            string token = await response.Content.ReadAsStringAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);

            return true;
        }

        _logger.LogError("Failed to login user. Status code: {StatusCode}", response.StatusCode);

        return false;
    }

    public async Task SetIdeaAsActiveAsync(IdeaModel idea)
    {
        _logger.LogInformation("Setting idea '{Id}' as active", idea.Id);

        var queryParamters = new Dictionary<string, string>
        {
            { "id", idea.Id }
        };

        var content = new FormUrlEncodedContent(queryParamters);
        var response = await _httpClient.PostAsync($"{_ideasEndpoint}/active", content);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to set idea '{Id}' as active. Status code: {StatusCode}", idea.Id, response.StatusCode);
        }
    }

    public async Task UpdateIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Updating idea '{Title}'", idea.Title);

        var response = await _httpClient.PutAsJsonAsync($"{_ideasEndpoint}/{idea.Id}", idea);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to updated idea '{Id}'. Status code: {StatusCode}", idea.Id, response.StatusCode);
        }
    }
}