namespace IntegrationService.Services;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly INotifierService _notifierService;
    private const string ApiKeyHeaderName = "X-API-KEY";
    private const string ApiKey = "f47ac10b58cc4372a5670e02b2c3d479";

    public ClientService(IHttpClientFactory httpClientFactory, INotifierService notifierService)
    {
        _httpClientFactory = httpClientFactory;
        _notifierService = notifierService;
    }

    private HttpClient CreateClientWithApiKey(string userApiKey)
    {
        var apikey = string.IsNullOrEmpty(userApiKey) ? ApiKey : userApiKey;
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add(ApiKeyHeaderName, apikey);
        return client;
    }

    public async Task<ResponseModel?> GetAsync(string requestUri)
    {
        var client = CreateClientWithApiKey(string.Empty);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.GetAsync(requestUri);
            if (httpResponseMessage.StatusCode is not HttpStatusCode.OK and not HttpStatusCode.BadRequest)
                return null;
            result = await httpResponseMessage.DeserializeContent<ResponseModel>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }

    public async Task<ResponseModel?> PostAsync<T>(string requestUri, string userApiKey, T content)
    {
        var client = CreateClientWithApiKey(userApiKey);
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.PostAsync(requestUri, jsonContent);
            if (httpResponseMessage.StatusCode is not HttpStatusCode.OK and not HttpStatusCode.BadRequest)
                return null;
            result = await httpResponseMessage.DeserializeContent<ResponseModel>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }

    public async Task<ResponseModel?> PutAsync<T>(string requestUri, string userApiKey, T content)
    {
        var client = CreateClientWithApiKey(userApiKey);
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.PutAsync(requestUri, jsonContent);
            if (httpResponseMessage.StatusCode is not HttpStatusCode.OK and not HttpStatusCode.BadRequest)
                return null;
            result = await httpResponseMessage.DeserializeContent<ResponseModel>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }

    public async Task<ResponseModel?> DeleteAsync(string requestUri, string userApiKey)
    {
        var client = CreateClientWithApiKey(userApiKey);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.DeleteAsync(requestUri);
            if (httpResponseMessage.StatusCode is not HttpStatusCode.OK and not HttpStatusCode.BadRequest)
                return null;
            result = await httpResponseMessage.DeserializeContent<ResponseModel>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }
}
