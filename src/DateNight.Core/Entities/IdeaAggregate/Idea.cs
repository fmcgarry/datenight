using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.IdeaAggregate;

public class Idea : BaseEntity, IAggregateRoot
{
    public Idea()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public IdeaState State { get; set; }
    public string Title { get; set; } = string.Empty;
}