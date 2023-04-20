using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using DateNight.Infrastructure.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace DateNight.Infrastructure.Repositories;

public class IdeaRepository : IIdeaRepository
{
    protected readonly Container _container;
    private readonly IAppLogger<IdeaRepository> _logger;
    private readonly DateNightDatabaseOptions _options;

    public IdeaRepository(IAppLogger<IdeaRepository> logger, IOptions<DateNightDatabaseOptions> options, CosmosClient cosmosClient)
    {
        _logger = logger;
        _options = options.Value;
        _container = cosmosClient.GetContainer(_options.DatabaseId, _options.IdeasContainer);
    }

    public async Task AddAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        await _container.CreateItemAsync(entity, null, null, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Idea entity, CancellationToken cancellationToken = default)
    {
        string id = entity.Id.ToString();
        var partitionKey = new PartitionKey(id);

        await _container.DeleteItemAsync<Idea>(id, partitionKey, null, cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Idea>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = new QueryDefinition("SELECT * FROM c ");
        IEnumerable<Idea> results = await QueryAsync(query);

        return results;
    }

    public async Task<IEnumerable<Idea>> GetAllUserIdeasAsync(string userId)
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.createdBy = @userId").WithParameter("userId", userId);
        IEnumerable<Idea> results = await QueryAsync(query);

        return results;
    }

    public async Task<Idea?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var partitionKey = new PartitionKey(id);
            var item = await _container.ReadItemAsync<Idea>(id, partitionKey, null, cancellationToken);

            return item;
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

    public async Task UpdateRangeAsync(IEnumerable<Idea> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected async Task<IEnumerable<Idea>> QueryAsync(QueryDefinition query)
    {
        var queryIterator = _container.GetItemQueryIterator<Idea>(query);

        List<Idea> results = new();

        while (queryIterator.HasMoreResults)
        {
            var response = await queryIterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }
}