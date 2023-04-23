namespace DateNight.Api.Data;

public class IdeaActions
{
    public record IdeaRequest(string Title, string Description);

    public record IdeaResponse(string Id, string Title, string Description, string CreatedBy, DateTime CreatedOn);
}