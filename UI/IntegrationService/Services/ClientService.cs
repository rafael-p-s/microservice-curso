﻿namespace IntegrationService.Services;

public class ClientService : IClientService
{
    public async Task<List<UserModel>> GetAllUsers()
    {
        //using (var client = new HttpClient())
        //{
        //    var response = await client.GetAsync("https://yourapi.com/api/ControllerName/ActionMethodName");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var data = await response.Content.ReadAsStringAsync();
        //        // Process the data
        //    }
        //}

        return UsersFaker.UsersList();
    }

    public async Task<UserModel?> GetUserById(int id)
    {
        return UsersFaker.UsersList().FirstOrDefault(x => x.Id == id);
    }

    public async Task<UserModel?> CreateUser(UserModel model)
    {
        return new();
    }

    public async Task<UserModel?> UpdateUser(UserModel model)
    {
        return null;
    }

    public async Task<bool> DeleteUser(int id)
    {
        return true;
    }

    public async Task<List<WeatherModel>> GetAllWeather()
    {
        return WeatherFaker.WeatherList();
    }

    public async Task<WeatherModel> GetWeatherByCity(string city)
    {
        return WeatherFaker.WeatherList().FirstOrDefault(x => x.City == city) ?? new();
    }
}