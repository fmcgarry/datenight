namespace DateNight.Api.Data;

public class IdeaActions
{
    public enum IdeaState
    {
        None,
        Active,
        Completed,
        Abandoned,
        Stolen
    }

    public record AddIdeaRequest(string Title, string Description);
    public record UpdateIdeaRequest(string Title, string Description, IdeaState State);
    public record IdeaResponse(string Id, string Title, string Description, string CreatedBy, DateTime CreatedOn, double PopularityScore);
}