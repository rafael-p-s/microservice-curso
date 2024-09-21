namespace Domain.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAll();
    Task<UserModel> GetUserById(int id);
    Task<UserModel> CreateUser(UserModel model);
    Task<UserModel> UpdateUser(UserModel model);
    Task<bool> DeleteUser(int id);
}
