namespace DateNight.Api.Data;

public class Idea
{
    public required DateTime CreatedOn { get; init; }
    public required string Description { get; init; }
    public required string Title { get; init; }
}