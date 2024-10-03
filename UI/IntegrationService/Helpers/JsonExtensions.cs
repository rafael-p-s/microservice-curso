namespace IntegrationService.Helpers;

public static class JsonExtensions
{
    public static StringContent SerializeContent<T>(T content)
    {
        return new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
    }

    public static async Task<T?> DeserializeContent<T>(this HttpResponseMessage httpResponseMessage)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception)
        {
            return default;
        }
    }

    public static T? DeserializeCustomResponse<T>(object response)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(response.ToString()!, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception)
        {
            return default;
        }
    }
}
