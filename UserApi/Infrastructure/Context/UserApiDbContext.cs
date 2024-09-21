namespace Infrastructure.Context;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> opt) : DbContext(opt)
{
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
