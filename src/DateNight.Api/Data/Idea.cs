namespace DateNight.Api.Data;

public class Idea
{
    public string CreatedBy { get; set; } = string.Empty;
    public required DateTime CreatedOn { get; init; }
    public required string Description { get; init; }
    public string? Id { get; init; }
    public required string Title { get; init; }
}