namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly INotifierService _notifierService;

    public UserService(IUserRepository userRepository, INotifierService notifierService)
    {
        _userRepository = userRepository;
        _notifierService = notifierService;
    }

    public async Task<User?> GetUserRolesAsync(string apiKey)
    {
        var result = await _userRepository.GetUserByApiKeyAsync(apiKey);

        if (result is null)
            return null;

        return result;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var result = await _userRepository.GetAll();

        return User.MapDtoList(result);
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var result = await _userRepository.GetById(id);

        if (result is null)
        {
            _notifierService.AddLog("User not found in database.");
            return null;
        }

        return User.MapDto(result);
    }

    public async Task<UserDto?> CreateUser(UserBaseDto userBaseDto)
    {
        var result = await _userRepository.Add(new(userBaseDto));

        if (result is null)
        {
            _notifierService.AddLog("User cannot be created in database.");
            return null;
        }

        return User.MapDto(result);
    }

    public async Task<UserDto?> UpdateUser(UserDto userDto)
    {
        var result = await _userRepository.Update(new(userDto));

        if (result is null)
        {
            _notifierService.AddLog("User cannot be updated in database.");
            return null;
        }

        return User.MapDto(result);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var getUser = await _userRepository.GetById(id);

        if (getUser is null)
        {
            _notifierService.AddLog("User cannot be found in database.");
            return false;
        }

        if (getUser.ApiKey.Equals("f47ac10b58cc4372a5670e02b2c3d479"))
        {
            _notifierService.AddLog("Super User cannot deleted.");

            return false;
        }

        var result = await _userRepository.Delete(getUser);

        if (!result)
        {
            _notifierService.AddLog("User cannot be deleted from database.");
            return false;
        }

        return true;
    }
}
