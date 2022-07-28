using MyBlazor.Shared;
using System.Net.Http.Json;

namespace MyBlazor.Client.Services;
public class FetchDataService : IFetchDataService
{
    private readonly HttpClient http;
    public FetchDataService(HttpClient http) => this.http = http;
    public Task<WeatherForecast[]?> GetWeatherForecastsAsync() =>
        http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
}