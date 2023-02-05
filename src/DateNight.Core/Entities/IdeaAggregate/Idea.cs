using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.IdeaAggregate;

public class Idea : BaseEntity<string>, IAggregateRoot
{
    public Idea()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Idea(string title, string description, DateTime createdOn) : this()
    {
        Title = title;
        Description = description;
        CreatedOn = createdOn;
    }

    public required DateTime CreatedOn { get; init; }
    public required string Description { get; init; }
    public required string Title { get; init; }
}