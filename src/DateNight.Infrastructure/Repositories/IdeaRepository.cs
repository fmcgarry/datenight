using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using DateNight.Infrastructure.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace DateNight.Infrastructure.Repositories;

public class IdeaRepository : IRepository<Idea>
{
    private readonly Container _container;
    private readonly IAppLogger<IdeaRepository> _logger;
    private readonly DateNightDatabaseOptions _options;
    private readonly PartitionKey _partitionKey;

    public IdeaRepository(IAppLogger<IdeaRepository> logger, IOptions<DateNightDatabaseOptions> options, CosmosClient cosmosClient)
    {
        _logger = logger;
        _options = options.Value;
        _container = cosmosClient.GetContainer(_options.DatabaseId, _options.ContainerId);
        _partitionKey = new PartitionKey("id");
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

    public async Task<Idea?> GetByIdAsync<U>(U id, CancellationToken cancellationToken = default)
    {
        try
        {
            var idea = await _container.ReadItemAsync<Idea>(id as string, _partitionKey, null, cancellationToken);
            return idea;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        await _container.UpsertItemAsync(entity, null, null, cancellationToken);
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