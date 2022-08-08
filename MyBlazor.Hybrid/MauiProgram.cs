using MyBlazor.Hybrid.Services;

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
            // If using localhost
            builder.Services.AddDevHttpClient(7030);
            // If using a published development API instead of localhost
            //builder.Configuration.AddJsonResource("MyBlazor.Hybrid.Development.appsettings.json");
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApiBaseAddress"]) });

#else 
            builder.Configuration.AddJsonResource("MyBlazor.Hybrid.appsettings.json");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApiBaseAddress"]) });
#endif
            builder.Services.AddScoped<IFetchDataService, FetchDataService>();

            return builder.Build();
        }
    }
}