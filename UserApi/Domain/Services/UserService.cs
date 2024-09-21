namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var result = await _userRepository.GetAll();

        return User.MapDtoList(result);
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var result = await _userRepository.GetById(id);

        return User.MapDto(result);
    }

    public async Task<UserDto?> CreateUser(UserBaseDto userBaseDto)
    {
        var result = await _userRepository.Add(new(userBaseDto));

        if (result is null)
            return null;

        return User.MapDto(result);
    }

    public async Task<UserDto?> UpdateUser(UserDto userDto)
    {
        var result = await _userRepository.Update(new(userDto));

        if (result is null)
            return null;

        return User.MapDto(result);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var getUser = await _userRepository.GetById(id);

        if (getUser is null)
            return false;

        return await _userRepository.Delete(getUser);
    }
}
