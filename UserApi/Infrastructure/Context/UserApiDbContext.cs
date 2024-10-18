namespace Infrastructure.Context;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> opt) : DbContext(opt)
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<User> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
