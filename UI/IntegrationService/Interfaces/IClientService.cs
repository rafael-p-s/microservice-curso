namespace IntegrationService.Interfaces;

public interface IClientService
{
    Task<ResponseModel?> GetAsync(string requestUri);
    Task<ResponseModel?> PostAsync<T>(string requestUri, string userApiKey, T content);
    Task<ResponseModel?> PutAsync<T>(string requestUri, string userApiKey, T content);
    Task<ResponseModel?> DeleteAsync(string requestUri, string userApiKey);
}
