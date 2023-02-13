using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using Microsoft.Azure.Cosmos;

namespace DateNight.Infrastructure.Repositories;

public class IdeaRepository : IRepository<Idea>
{
    private readonly Container _container;
    private readonly IAppLogger<IdeaRepository> _logger;

    public IdeaRepository(IAppLogger<IdeaRepository> logger, CosmosClient cosmosClient)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("fmcgarry", "datenight");
    }

    public async Task AddAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        await _container.CreateItemAsync(entity, null, null, cancellationToken);
    }

    public Task<IEnumerable<Idea>> AddRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Idea?> GetAllAsync<U>(U id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Idea?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}