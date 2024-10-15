namespace Infrastructure.Context.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("USER_ROLES");

        builder.HasKey(ur => ur.Id);

        builder.Property(ur => ur.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID");

        builder.Property(ur => ur.Name)
            .HasColumnName("NAME")
            .IsRequired();

        builder.Property(ur => ur.UserId)
            .HasColumnName("USER_ID")
            .IsRequired();

        builder.HasOne<User>() // Specify the related entity type
            .WithMany() // Specify the relationship (many UserRoles to one User)
            .HasForeignKey(ur => ur.UserId) // Set up the foreign key
            .HasConstraintName("FK_USER_ROLE_USER");
    }
}
