using System.Net.Security;

namespace MyBlazor.Hybrid.Services
{
    public class DevHttpsConnectionHelper
    {
        public DevHttpsConnectionHelper(int sslPort)
        {
            DevServerRootUrl = new UriBuilder("https", DevServerName, sslPort).Uri.ToString();
#if WINDOWS
            LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient());
#else 
            LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient(GetPlatformMessageHandler()));
#endif
        }

        public string DevServerName =>
#if WINDOWS
            "localhost";
#elif ANDROID
            "10.0.2.2";
#else
            throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
#endif

        public string DevServerRootUrl { get; }

        private Lazy<HttpClient> LazyHttpClient;
        public HttpClient HttpClient => LazyHttpClient.Value;

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new CustomAndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == SslPolicyErrors.None;
            };
            return handler;

#else
            throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
#endif
        }

#if ANDROID
        internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
        {
            protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
                => new CustomHostnameVerifier();

            private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
            {
                public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
                {
                    return
                        Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
                        || hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
                }
            }
        }
#endif
    }

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