namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(UserApiDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }

    public async Task<User> GetUserByApiKeyAsync(string apiKey)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.ApiKey == apiKey);

        return result;
    }
}
