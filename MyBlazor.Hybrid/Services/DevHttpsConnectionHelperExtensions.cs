namespace MyBlazor.Hybrid.Services
{
    public static class DevHttpsConnectionHelperExtensions
    {
        /// <summary>
        /// Configures HttpClient to use localhost or 10.0.2.2 and bypass certificate checking on Android.
        /// </summary>
        /// <param name="sslPort">Development server port</param>
        /// <returns>The IServiceCollection</returns>
        public static IServiceCollection AddDevHttpClient(this IServiceCollection services, int sslPort)
        {
            var devSslHelper = new DevHttpsConnectionHelper(sslPort);
            var http = devSslHelper.HttpClient;
            http.BaseAddress = new Uri(devSslHelper.DevServerRootUrl);
            services.AddScoped(sp => http);
            return services;
        }
    }
}