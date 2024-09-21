namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IClientService _clientService;

    public UserService(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<List<UserModel>> GetAll()
    {
        var result = await _clientService.GetAllUsers();

        if (result is not null)
            return result;

        return [];
    }

    public async Task<UserModel?> GetUserById(int id)
    {
        return await _clientService.GetUserById(id);
    }

    public async Task<UserModel?> CreateUser(UserModel model)
    {
        return await _clientService.CreateUser(model);
    }

    public async Task<UserModel?> UpdateUser(UserModel model)
    {
        return await _clientService.UpdateUser(model);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await _clientService.DeleteUser(id);
    }
}
