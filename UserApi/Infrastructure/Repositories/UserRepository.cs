namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {

    }
}
