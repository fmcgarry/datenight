﻿using DateNight.App.Interfaces;
using DateNight.App.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DateNight.App.Clients.DateNightApi;

internal class DateNightApiClient : IDateNightApiClient
{
    public const string HttpClientBaseAddress = "https://localhost:7000/";
    public const string HttpClientName = "DateNightApiClient";

    private const string _ideasEndpoint = "ideas";

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

    public async Task DeleteIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Deleting idea '{Id}'", idea.Id);

        var response = await _httpClient.DeleteAsync($"{_ideasEndpoint}/{idea.Id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to delete idea '{Id}'. Status code: {StatusCode}", idea.Id, response.StatusCode);
        }
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

    public async Task UpdateIdeaAsync(IdeaModel idea)
    {
        _logger.LogInformation("Updating idea '{Title}'", idea.Title);
    }
}