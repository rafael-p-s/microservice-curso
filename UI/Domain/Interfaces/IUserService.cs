namespace Domain.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAll();
    Task<UserModel> GetUserById(int id);
}
