namespace Domain.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAll();
    Task<UserDto?> GetUserById(int id);
}
