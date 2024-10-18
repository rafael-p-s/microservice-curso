namespace IntegrationService.Interfaces;

public interface IClientService
{
    Task<ResponseDto?> GetAsync(string requestUri);
    Task<ResponseDto?> PostAsync<T>(string requestUri, T content);
    Task<ResponseDto?> PutAsync<T>(string requestUri, T content);
    Task<ResponseDto?> DeleteAsync(string requestUri);
}
