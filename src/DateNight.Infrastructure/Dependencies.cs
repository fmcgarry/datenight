﻿using Azure.Identity;
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
        string? databaseConnectionString = config.GetConnectionString("DateNightDatabase");

        if (databaseConnectionString is null)
        {
            services.AddSingleton<IIdeaRepository, IdeaMemoryRepository>();
        }
        else
        {
            var cosmosClientOptions = new CosmosClientOptions() { SerializerOptions = new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase } };

            services.AddSingleton(new CosmosClient(databaseConnectionString, cosmosClientOptions));
            services.AddOptions<DateNightDatabaseOptions>().Bind(config.GetSection(DateNightDatabaseOptions.DateNightDatabase));
            services.AddTransient<IIdeaRepository, IdeaRepository>();
        }

        services.AddTransient<IIdeaService, IdeaService>();

        return services;
    }

    public static IConfigurationBuilder AddRequiredInfrastructureConfiguration(this IConfigurationBuilder builder)
    {
        var config = builder.Build();

        string? keyVaultName = config.GetValue<string>("KeyVaultName");

        if (keyVaultName is not null)
        {
            string url = $"https://{keyVaultName}.vault.azure.net/";
            builder.AddAzureKeyVault(new Uri(url), new DefaultAzureCredential());
        }

        return builder;
    }

    public static IServiceCollection AddRequiredInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }

    public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration config)
    {
        string? databaseConnectionString = config.GetConnectionString("DateNightDatabase");

        if (databaseConnectionString is null)
        {
            services.AddSingleton<IUserRepository, UserMemoryRepository>();
        }
        else
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}