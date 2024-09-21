namespace Infrastructure.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedOnAdd().HasColumnName("ID");

        builder.Property(t => t.Name).HasColumnName("NAME").IsRequired();

        builder.Property(t => t.Age).HasColumnName("AGE").IsRequired();

        builder.Property(t => t.Email).HasColumnName("EMAIL").IsRequired();

        builder.Property(t => t.Address).HasColumnName("ADDRESS").IsRequired();

        builder.Property(t => t.City).HasColumnName("CITY").IsRequired();

        builder.Property(t => t.Country).HasColumnName("COUNTRY").IsRequired();

        builder.Property(t => t.PostalCode).HasColumnName("POSTAL_CODE").IsRequired();
    }
}
