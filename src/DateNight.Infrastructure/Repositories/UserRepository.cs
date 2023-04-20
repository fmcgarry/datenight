using DateNight.Core.Interfaces;
using DateNight.Infrastructure.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using User = DateNight.Core.Entities.UserAggregate.User;

namespace DateNight.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAppLogger<UserRepository> _logger;
    protected readonly Container _container;
    private readonly DateNightDatabaseOptions _options;

    public UserRepository(IAppLogger<UserRepository> logger, IOptions<DateNightDatabaseOptions> options, CosmosClient cosmosClient)
    {
        _logger = logger;
        _options = options.Value;
        _container = cosmosClient.GetContainer(_options.DatabaseId, _options.UsersContainer);
    }

    public async Task<User?> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        await _container.CreateItemAsync(entity, null, null, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        string id = entity.Id.ToString();
        var partitionKey = new PartitionKey(id);

        await _container.DeleteItemAsync<User>(id, partitionKey, null, cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = new QueryDefinition("SELECT * FROM c ");
        IEnumerable<User> results = await QueryAsync(query);

        return results;
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var partitionKey = new PartitionKey(id);
            var item = await _container.ReadItemAsync<User>(id, partitionKey, null, cancellationToken);

            return item;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        await _container.UpsertItemAsync(entity, null, null, cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected async Task<IEnumerable<User>> QueryAsync(QueryDefinition query)
    {
        var queryIterator = _container.GetItemQueryIterator<User>(query);

        List<User> results = new();

        while (queryIterator.HasMoreResults)
        {
            var response = await queryIterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }
}