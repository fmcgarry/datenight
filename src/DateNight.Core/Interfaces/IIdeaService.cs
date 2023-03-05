using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IIdeaService
    {
        Task AddIdeaAsync(Idea idea);

        /// <summary>
        /// Delete an Idea.
        /// </summary>
        /// <param name="id">The idea id.</param>
        /// <exception cref="ArgumentException">When id is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When an idea with id is not found.</exception>
        /// <returns></returns>
        Task DeleteIdeaAsync(string id);

        /// <summary>
        /// Get the currently active idea.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">No idea is active.</exception>
        Task<Idea> GetActiveIdeaAsync();

        Task<IEnumerable<Idea>> GetAllIdeasAsync();

        Task<Idea> GetIdeaByIdAsync(string id);

        Task<Idea> GetRandomIdeaAsync();

        /// <summary>
        /// Set the Idea with the passed id as active.
        /// </summary>
        /// <param name="id">The id of the Idea.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When id is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When an idea with id is not found.</exception>
        /// <exception cref="ArgumentException">When an idea with id is not found.</exception>
        Task SetActiveIdea(string id);

        Task UpdateIdeaAsync(Idea idea);
    }
}