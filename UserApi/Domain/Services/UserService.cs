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

        return User.MapEntityList(result);
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        var result = await _userRepository.GetById(id);

        return User.MapEntity(result);
    }
}
