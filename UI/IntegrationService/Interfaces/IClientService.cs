namespace IntegrationService.Interfaces;

public interface IClientService
{
    Task<List<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(int id);
    Task<List<WeatherModel>> GetAllWeather();
    Task<WeatherModel> GetWeatherByCity(string city);
}
