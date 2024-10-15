namespace Infrastructure.Repositories;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(UserApiDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}