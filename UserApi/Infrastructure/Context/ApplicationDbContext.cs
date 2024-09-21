namespace Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : DbContext(opt)
{
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
