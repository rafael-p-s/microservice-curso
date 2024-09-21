namespace Domain.Interfaces;

public interface IWeatherService
{
    Task<List<WeatherModel>> GetAll();
    Task<WeatherModel> GetByCity(string city);
}
