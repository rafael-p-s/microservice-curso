namespace Shared.Mock;

public static class WeatherFaker
{
    public static WeatherModel Weather(string city = "Maringá", double temp = 10)
    {
        return new()
        {
            City = city,
            Description = temp > 20 ? "Sunny" : "Cloudy",
            Temperature = temp
        };
    }

    public static List<WeatherModel> WeatherList()
    {
        return [Weather(), Weather("Presidente Prudente", 30), Weather("Austin", -5)];
    }
}
