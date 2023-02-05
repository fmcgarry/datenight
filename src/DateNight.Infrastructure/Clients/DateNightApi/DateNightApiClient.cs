using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace DateNight.Infrastructure.Clients.DateNightApi;

public class DateNightApiClient : IDateNightApiClient
{
    public const string HttpClientName = "DateNightApiClient";

    private readonly HttpClient _httpClient;
    private readonly IAppLogger<DateNightApiClient> _logger;
    private readonly DateNightApiClientOptions _options;

    public DateNightApiClient(IAppLogger<DateNightApiClient> logger, IOptions<DateNightApiClientOptions> options, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _options = options.Value;

        _httpClient = httpClientFactory.CreateClient(HttpClientName);
        _httpClient.BaseAddress = new Uri(_options.BaseAddress);

        _logger.LogInformation("HttpClient {0} registered with base address: {1}", HttpClientName, _options.BaseAddress);
    }

    public async Task CreateIdeaAsync(Idea idea)
    {
        var response = await _httpClient.PostAsJsonAsync("ideas", idea);
    }
}