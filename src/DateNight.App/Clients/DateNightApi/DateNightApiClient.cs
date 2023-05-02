using DateNight.App.Components.IdeaComponent;
using DateNight.App.Models;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace DateNight.App.Clients.DateNightApi;

internal class DateNightApiClient : IDateNightApiClient
{
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

    public async Task AddPartner(string code)
    {
        _logger.LogInformation("Adding partner using code '{code}'", code);

        string id = GetUserIdFromToken();

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "code", code }
        });

        var response = await _httpClient.PostAsync($"{_usersEndpoint}/{id}/partners", content);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to add partner using code '{code}'. Status code: {StatusCode}", code, response.StatusCode);
        }
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

        if (idea is not null)
        {
            ConvertToLocalTime(idea);
        }

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

        if (ideas is not null)
        {
            ConvertToLocalTime(ideas);
            return ideas;
        }

        return Enumerable.Empty<IdeaModel>();
    }

    public async Task<UserModel?> GetCurrentUserAsync()
    {
        _logger.LogInformation("Getting current user");

        string id = GetUserIdFromToken();
        var user = await GetUserAsync(id);

        return user;
    }

    public async Task<IdeaModel?> GetIdeaAsync(string id)
    {
        _logger.LogInformation("Getting idea '{Id}'", id);

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get idea '{Id}'. Status code: {StatusCode}", id, response.StatusCode);
        }

        var idea = await response.Content.ReadFromJsonAsync<IdeaModel>();

        if (idea is not null)
        {
            ConvertToLocalTime(idea);
        }

        return idea;
    }

    public async Task<IdeaModel?> GetRandomIdeaAsync()
    {
        _logger.LogInformation("Getting random idea");

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/random");

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // no ideas were found
                return null;
            }

            _logger.LogError("Failed to get a random idea. Status code: {StatusCode}", response.StatusCode);
        }

        var idea = await response.Content.ReadFromJsonAsync<IdeaModel>();

        if (idea is not null)
        {
            ConvertToLocalTime(idea);
        }

        return idea;
    }

    public async Task<IEnumerable<IdeaModel>> GetTopIdeasAsync(int start, int end)
    {
        _logger.LogInformation("Getting top ideas");

        var response = await _httpClient.GetAsync($"{_ideasEndpoint}/top?start={start}&end={end}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get top ideas. Status code: {StatusCode}", response.StatusCode);
        }

        var ideas = await response.Content.ReadFromJsonAsync<IEnumerable<IdeaModel>>();

        if (ideas is not null)
        {
            return ConvertToLocalTime(ideas);
        }

        return Enumerable.Empty<IdeaModel>();
    }

    public async Task<UserModel?> GetUserAsync(string id)
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
            string responseContent = await response.Content.ReadAsStringAsync();
            var token = JsonNode.Parse(responseContent)!["token"]!.GetValue<string>();

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);

            return true;
        }

        _logger.LogError("Failed to login user. Status code: {StatusCode}", response.StatusCode);

        return false;
    }

    public async Task RemovePartner(string id)
    {
        string userId = GetUserIdFromToken();

        var response = await _httpClient.DeleteAsync($"{_usersEndpoint}/{userId}/partners/{id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to remove partner '{id}'. Status code: {StatusCode}", id, response.StatusCode);
        }
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

    public async Task UpdateUserAsync(string name, string email)
    {
        await UpdateUserInternalAsync(name, email, string.Empty);
    }

    public async Task UpdateUserInternalAsync(string name, string email, string password)
    {
        string id = GetUserIdFromToken();

        var content = JsonContent.Create(new
        {
            Email = email,
            Password = password,
            Name = name
        });

        var response = await _httpClient.PutAsync($@"{_usersEndpoint}\{id}", content);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to update user. Status code: {StatusCode}", response.StatusCode);
        }
    }

    public async Task UpdateUserPasswordAsync(string password)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(_httpClient.DefaultRequestHeaders.Authorization!.Parameter);

        string email = token.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;

        await UpdateUserInternalAsync(string.Empty, string.Empty, password);
        await LoginUserAsync(email, password);
    }

    private static IEnumerable<IdeaModel> ConvertToLocalTime(IEnumerable<IdeaModel> ideas)
    {
        foreach (var idea in ideas)
        {
            ConvertToLocalTime(idea);
        }

        return ideas;
    }

    private static void ConvertToLocalTime(IdeaModel idea)
    {
        idea.CreatedOn = idea.CreatedOn.ToLocalTime();
    }

    private string GetUserIdFromToken()
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(_httpClient.DefaultRequestHeaders.Authorization!.Parameter);
        string id = token.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

        return id;
    }
}