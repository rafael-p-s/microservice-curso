namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IClientService _clientService;
    private readonly INotifierService _notifierService;

    private const string GET_ALL_URL = "https://localhost:7140/User/GetAll";
    private const string GET_BY_ID_URL = "https://localhost:7140/User/Details/";
    private const string CREATE_USER_URL = "https://localhost:7140/User/Create";
    private const string UPDATE_USER_URL = "https://localhost:7140/User/Update";
    private const string DELETE_USER_BY_ID_URL = "https://localhost:7140/User/Delete/";

    public UserService(IClientService clientService, INotifierService notifierService)
    {
        _clientService = clientService;
        _notifierService = notifierService;
    }

    public async Task<List<UserModel>?> GetAll()
    {
        var result = await _clientService.GetAsync(GET_ALL_URL);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return [];
        }

        return JsonExtensions.DeserializeCustomResponse<List<UserModel>>(result.Content!);
    }

    public async Task<UserModel?> GetUserById(int id)
    {
        var url = string.Concat(GET_BY_ID_URL, id);
        var result = await _clientService.GetAsync(url);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<UserModel>(result.Content!);
    }

    public async Task<UserModel?> CreateUser(UserBaseModel userModel)
    {
        var result = await _clientService.PostAsync(CREATE_USER_URL, userModel);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<UserModel>(result.Content!);
    }

    public async Task<UserModel?> UpdateUser(UserModel userModel)
    {
        var result = await _clientService.PutAsync(UPDATE_USER_URL, userModel);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<UserModel>(result.Content!);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var url = string.Concat(DELETE_USER_BY_ID_URL, id);
        var result = await _clientService.DeleteAsync(url);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<bool>(result.Content!);
    }
}
