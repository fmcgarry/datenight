using DateNight.Core.Entities.IdeaAggregate;

namespace DateNight.Core.Interfaces
{
    public interface IIdeaService
    {
        /// <summary>
        /// Set the Idea with the passed id as active.
        /// </summary>
        /// <param name="id">The id of the Idea.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When id is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When an idea with id is not found.</exception>
        /// <exception cref="ArgumentException">When an idea with id is not found.</exception>
        Task ActivateIdea(string id);

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
        Task<Idea> GetUserActiveIdeaAsync(string? userId);

        Task<IEnumerable<Idea>> GetAllUserIdeasAsync(string? userId);

        Task<Idea> GetIdeaByIdAsync(string id);

        Task<Idea> GetRandomUserIdeaAsync(string userId);

        Task UpdateIdeaAsync(Idea idea);
    }
}