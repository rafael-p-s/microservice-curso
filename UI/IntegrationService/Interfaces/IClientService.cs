namespace IntegrationService.Interfaces;

public interface IClientService
{
    Task<ResponseModel?> GetAsync(string requestUri);
    Task<ResponseModel?> PostAsync<T>(string requestUri, T content);
    Task<ResponseModel?> PutAsync<T>(string requestUri, T content);
    Task<ResponseModel?> DeleteAsync(string requestUri);
}
