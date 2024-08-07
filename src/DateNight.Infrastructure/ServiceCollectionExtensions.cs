﻿using DateNight.Core.Interfaces;
using DateNight.Core.Services;
using DateNight.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DateNight.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdeaService(this IServiceCollection services, IConfiguration configurationSection)
    {
        string? databaseConnectionString = GetDateNightCosmosDbConnectionString(configurationSection);

        if (string.IsNullOrEmpty(databaseConnectionString))
        {
            services.AddSingleton<IIdeaRepository, IdeaMemoryRepository>();
        }
        else
        {
            services.AddDateNightCosmosDbClient(databaseConnectionString);
            services.AddOptionsFromConfigurationSection<DateNightDatabaseOptions>(configurationSection);
            services.AddTransient<IIdeaRepository, IdeaRepository>();
        }

        services.AddTransient<IIdeaService, IdeaService>();

        return services;
    }

    public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration databaseOptionsSection)
    {
        string? databaseConnectionString = GetDateNightCosmosDbConnectionString(databaseOptionsSection);

        if (string.IsNullOrEmpty(databaseConnectionString))
        {
            services.AddSingleton<IUserRepository, UserMemoryRepository>();
        }
        else
        {
            services.AddDateNightCosmosDbClient(databaseConnectionString);
            services.AddOptionsFromConfigurationSection<DateNightDatabaseOptions>(databaseOptionsSection);
            services.AddTransient<IUserRepository, UserRepository>();
        }

        services.AddTransient<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddDateNightCosmosDbClient(this IServiceCollection services, string connectionString)
    {
        if (connectionString is not null)
        {
            var cosmosClientOptions = new CosmosClientOptions() { SerializerOptions = new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase } };
            services.TryAddSingleton(new CosmosClient(connectionString, cosmosClientOptions));
        }

        return services;
    }

    private static IServiceCollection AddOptionsFromConfigurationSection<T>(this IServiceCollection services, IConfiguration configurationSection) where T : class
    {
        // Don't register more than 1 options instance.
        if (!services.Any(x => x.ServiceType == typeof(IConfigureOptions<T>)))
        {
            services.AddOptions<T>()
                    .Bind(configurationSection)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
        }

        return services;
    }

    private static string? GetDateNightCosmosDbConnectionString(IConfiguration configurationSection)
    {
        return configurationSection["ConnectionString"];
    }
}