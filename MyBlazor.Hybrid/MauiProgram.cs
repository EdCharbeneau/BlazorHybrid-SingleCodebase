using Microsoft.AspNetCore.Components.WebView.Maui;
using MyBlazor.Hybrid.Services;
using MyBlazor.Shared;

namespace MyBlazor.Hybrid
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
#endif


            // removed this registration
            // builder.Services.AddSingleton<IFetchDataService>();

            // BaseAddress should match the url and port of the server application
            // Configuration files can be used to control the BaseAddress value instead of hard coding the value
#if DEBUG
#if ANDROID || IOS
            builder.Services.AddDevHttpClient(7030);
#else
                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7030/") });
#endif
#else
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7030/") });
#endif
            builder.Services.AddScoped<IFetchDataService, FetchDataService>();

            return builder.Build();
        }
    }
}