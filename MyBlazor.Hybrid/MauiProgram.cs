using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using MyBlazor.Hybrid.Services;
using MyBlazor.Shared;
using System.Reflection;

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
            var a = Assembly.GetExecutingAssembly();

#if DEBUG
            var configResourceName = "MyBlazor.Hybrid.appsettings.Development.json";
#else
            var configResourceName = "MyBlazor.Hybrid.appsettings.json";
#endif

            using var stream = a.GetManifestResourceStream("MyBlazor.Hybrid.appsettings.json");

            builder.Configuration.AddJsonStream(stream);

            // removed this registration
            // builder.Services.AddSingleton<IFetchDataService>();

            // BaseAddress should match the url and port of the server application
            // Configuration files can be used to control the BaseAddress value instead of hard coding the value
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApiBaseAddress"]) });
            builder.Services.AddScoped<IFetchDataService, FetchDataService>();

            return builder.Build();
        }
    }
}