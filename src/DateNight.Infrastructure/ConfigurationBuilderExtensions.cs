using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace DateNight.Infrastructure;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddAppKeyVault(this IConfigurationBuilder builder)
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
}
