namespace Domain.Interfaces;

public interface IUserService
{
    Task<List<UserModel>?> GetAll();
    Task<UserModel?> GetUserById(int id);
    Task<UserModel?> CreateUser(UserBaseModel userModel);
    Task<UserModel?> UpdateUser(UserModel userModel);
    Task<bool> DeleteUser(int id);
}
