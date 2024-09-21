namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(UserApiDbContext applicationDbContext)
        : base(applicationDbContext)
    {

    }
}
