namespace Domain.Interfaces;

public interface IUserService
{
    Task<User?> GetUserRolesAsync(string apiKey);
    Task<List<UserDto>> GetAll();
    Task<UserDto?> GetUserById(int id);
    Task<UserDto?> CreateUser(UserBaseDto userBaseDto);
    Task<UserDto?> UpdateUser(UserDto userDto);
    Task<bool> DeleteUser(int id);
}
