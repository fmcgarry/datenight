namespace DateNight.Api.Data;

public class Idea
{
    public required string CreatedBy { get; init; }
    public required DateTime CreatedOn { get; init; }
    public required string Description { get; init; }
    public string? Id { get; init; }
    public required string Title { get; init; }
}