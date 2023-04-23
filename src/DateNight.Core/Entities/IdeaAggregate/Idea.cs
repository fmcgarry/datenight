using DateNight.Core.Interfaces;

namespace DateNight.Core.Entities.IdeaAggregate;

public class Idea : BaseEntity, IAggregateRoot
{
    public Idea()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public int AbandonCount { get; set; }
    public int CompletionCount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public double PopularityScore => (1 + StolenCount) * (double)CompletionCount / (1 + AbandonCount);
    public IdeaState State { get; set; }
    public int StolenCount { get; set; }
    public string Title { get; set; } = string.Empty;
}