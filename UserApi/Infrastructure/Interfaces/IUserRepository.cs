namespace Infrastructure.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByApiKeyAsync(string apiKey);
}
