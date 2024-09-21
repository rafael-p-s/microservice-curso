namespace UI.Controllers;

public class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Weather";

        var result = await _weatherService.GetAll();

        return View("Index", result);
    }

    public async Task<IActionResult> Details(string city)
    {
        ViewData["Title"] = "Weather Details";

        var result = await _weatherService.GetByCity(city);

        return View("Details", result);
    }
}
