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
#else 
            // If using a published development API instead of localhost
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("Production API") });
#endif
            builder.Services.AddScoped<IFetchDataService, FetchDataService>();

            return builder.Build();
        }
    }
}