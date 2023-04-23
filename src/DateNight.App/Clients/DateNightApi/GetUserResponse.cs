namespace DateNight.App.Clients.DateNightApi;

internal class GetUserResponse
{
    public string Email { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Partners { get; set; } = new();
}
