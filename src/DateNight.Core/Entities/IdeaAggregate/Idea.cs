using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.IdeaAggregate;

public class Idea : BaseEntity<string>, IAggregateRoot
{
    public Idea()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public Idea(string title, string description, DateTime createdOn) : this()
    {
        Title = title;
        Description = description;
        CreatedOn = createdOn;
    }

    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}