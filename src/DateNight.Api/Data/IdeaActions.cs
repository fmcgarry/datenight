namespace DateNight.Api.Data;

public class IdeaActions
{
    public record AddIdeaRequest(string Title, string Description);
}