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

    public async Task<IEnumerable<Idea>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Idea> results = await QueryIdeasAsync("Select * from c");

        return results;
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

    private async Task<IEnumerable<Idea>> QueryIdeasAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<Idea>(new QueryDefinition(queryString));

        List<Idea> results = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }
}