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

    public async Task<ResponseDto?> GetAsync(string requestUri)
    {
        var client = CreateClientWithApiKey();
        ResponseDto? result;

        try
        {
            var httpResponseMessage = await client.GetAsync(requestUri);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

            result = await httpResponseMessage.DeserializeContent<ResponseDto>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }

        return result;
    }

    public async Task<ResponseDto?> PostAsync<T>(string requestUri, T content)
    {
        var client = CreateClientWithApiKey();
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseDto? result;

        try
        {
            var httpResponseMessage = await client.PostAsync(requestUri, jsonContent);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

            result = await httpResponseMessage.DeserializeContent<ResponseDto>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }

        return result;
    }

    public async Task<ResponseDto?> PutAsync<T>(string requestUri, T content)
    {
        var client = CreateClientWithApiKey();
        StringContent jsonContent = JsonExtensions.SerializeContent(content);
        ResponseDto? result;

        try
        {
            var httpResponseMessage = await client.PutAsync(requestUri, jsonContent);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

            result = await httpResponseMessage.DeserializeContent<ResponseDto>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }

        return result;
    }

    public async Task<ResponseDto?> DeleteAsync(string requestUri)
    {
        var client = CreateClientWithApiKey();
        ResponseDto? result;

        try
        {
            var httpResponseMessage = await client.DeleteAsync(requestUri);
            var response = CheckStatusCodes(httpResponseMessage);

            if (response is not null)
                return response;

            result = await httpResponseMessage.DeserializeContent<ResponseDto>();
        }
        catch (Exception)
        {
            _notifierService.AddLog("Error to connect with backend app");
            return null;
        }
        return result;
    }

    private ResponseDto? CheckStatusCodes(HttpResponseMessage httpResponseMessage)
    {
        return (int)httpResponseMessage.StatusCode switch
        {
            403 => new ResponseDto(false, "Forbidden - 403: The user does not have the necessary permissions for the resource."),
            404 => new ResponseDto(false, "NotFound - 404: The requested resource could not be found on the server."),
            500 => new ResponseDto(false, "InternalServerError - 500: The server encountered an error and could not complete the request."),
            _ => null
        };
    }
}
