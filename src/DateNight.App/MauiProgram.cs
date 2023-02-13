using DateNight.App.Clients.DateNightApi;
using DateNight.App.Interfaces;
using Microsoft.Extensions.Logging;

namespace DateNight.App
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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<IDateNightApiClient, DateNightApiClient>();
            builder.Services.AddHttpClient(DateNightApiClient.HttpClientName).ConfigureHttpClient(x =>
            {
                x.BaseAddress = new Uri(DateNightApiClient.HttpClientBaseAddress);
            });

            return builder.Build();
        }
    }
}