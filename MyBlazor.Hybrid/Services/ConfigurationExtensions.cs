using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MyBlazor.Hybrid.Services
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds a JSON configuration source to builder
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configResourceName"></param>
        /// <returns>The IConfigurationBuilder</returns>
        public static IConfigurationBuilder AddJsonResource(this IConfigurationBuilder config, string configResourceName)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(configResourceName);
            config.AddJsonStream(stream);
            return config;
        }

    }
}
