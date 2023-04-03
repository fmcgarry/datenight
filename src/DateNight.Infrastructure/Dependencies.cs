﻿using Azure.Identity;
using DateNight.Core.Entities.IdeaAggregate;
using DateNight.Core.Interfaces;
using DateNight.Core.Services;
using DateNight.Infrastructure.Logging;
using DateNight.Infrastructure.Options;
using DateNight.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DateNight.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddIdeaService(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("DateNightDatabase") ?? string.Empty;
        var cosmosClientOptions = new CosmosClientOptions() { SerializerOptions = new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase } };

        services.AddSingleton(new CosmosClient(connectionString, cosmosClientOptions));
        services.AddOptions<DateNightDatabaseOptions>().Bind(config.GetSection(DateNightDatabaseOptions.DateNightDatabase));
        services.AddTransient<IRepository<Idea>, IdeaRepository>();

        services.AddTransient<IIdeaService, IdeaService>();

        return services;
    }

    public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IRepository<Core.Entities.UserAggregate.User>, UserRepository>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddRequiredInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }

    public static IConfigurationBuilder AddRequiredInfrastructureConfiguration(this IConfigurationBuilder builder)
    {
        var config = builder.Build();

        string url = $"https://{config["KeyVaultName"]}.vault.azure.net/";
        builder.AddAzureKeyVault(new Uri(url), new DefaultAzureCredential());

        return builder;
    }
}