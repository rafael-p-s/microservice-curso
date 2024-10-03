namespace IntegrationService.Services;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly INotifierService _notifierService;

    public ClientService(IHttpClientFactory httpClientFactory, INotifierService notifierService)
    {
        _httpClientFactory = httpClientFactory;
        _notifierService = notifierService;
    }

    public async Task<ResponseModel?> GetAsync(string requestUri)
    {
        var client = _httpClientFactory.CreateClient();
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
    public async Task<ResponseModel?> PostAsync<T>(string requestUri, T content)
    {
        var client = _httpClientFactory.CreateClient();
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
    public async Task<ResponseModel?> PutAsync<T>(string requestUri, T content)
    {
        var client = _httpClientFactory.CreateClient();
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

    public async Task<ResponseModel?> DeleteAsync(string requestUri)
    {
        var client = _httpClientFactory.CreateClient();
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
