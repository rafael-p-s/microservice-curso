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

    private HttpClient CreateClientWithApiKey()
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add(ApiKeyHeaderName, ApiKey);
        return client;
    }

    public async Task<ResponseModel?> GetAsync(string requestUri)
    {
        var client = CreateClientWithApiKey();
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.GetAsync(requestUri);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

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
        var client = CreateClientWithApiKey();
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.PostAsync(requestUri, jsonContent);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

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
        var client = CreateClientWithApiKey();
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.PutAsync(requestUri, jsonContent);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

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
        var client = CreateClientWithApiKey();
        ResponseModel? result;
        try
        {
            var httpResponseMessage = await client.DeleteAsync(requestUri);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

            result = await httpResponseMessage.DeserializeContent<ResponseModel>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }

    private ResponseModel? CheckStatusCodes(HttpResponseMessage httpResponseMessage)
    {
        return (int)httpResponseMessage.StatusCode switch
        {
            403 => new ResponseModel(false, "Forbidden - 403: The user does not have the necessary permissions for the resource."),
            404 => new ResponseModel(false, "NotFound - 404: The requested resource could not be found on the server."),
            500 => new ResponseModel(false, "InternalServerError - 500: The server encountered an error and could not complete the request."),
            _ => null
        };
    }
}
