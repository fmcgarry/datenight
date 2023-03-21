using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.IdeaAggregate;

public class Idea : BaseEntity<string>, IAggregateRoot
{
    public Idea()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public Idea(string id, string title, string description, DateTime createdOn, Guid createdBy) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public IdeaState State { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
}