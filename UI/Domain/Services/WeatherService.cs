namespace Domain.Services;

public class WeatherService : IWeatherService
{
    private readonly IClientService _clientService;

    public WeatherService(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<List<WeatherModel>> GetAll()
    {
        return await _clientService.GetAllWeather();
    }

    public async Task<WeatherModel> GetByCity(string city)
    {
        return await _clientService.GetWeatherByCity(city);
    }
}
