using DateNightApp.Clients.DateNightApi;
using Microsoft.Extensions.Logging;

namespace DateNightApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            string dateNightApiBaseAddress;
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            dateNightApiBaseAddress = "https://localhost:7000/";
#else
            dateNightApiBaseAddress = "https://fmcgarry-datenight.azurewebsites.net/";
#endif
            builder.Services.AddSingleton<IDateNightApiClient, DateNightApiClient>();
            builder.Services.AddHttpClient(DateNightApiClient.HttpClientName).ConfigureHttpClient(x =>
            {
                x.BaseAddress = new Uri(dateNightApiBaseAddress);
            });

            return builder.Build();
        }
    }
}