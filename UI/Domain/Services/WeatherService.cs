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
        return WeatherFaker.WeatherList();
    }

    public async Task<WeatherModel?> GetByCity(string city)
    {
        return WeatherFaker.WeatherList().FirstOrDefault(c => c.City == city);
    }
}
