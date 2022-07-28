using MyBlazor.Shared;
public interface IFetchDataService
{
    Task<WeatherForecast[]?> GetWeatherForecastsAsync();
}