namespace Infrastructure.Context.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("PRODUCTS");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedOnAdd().HasColumnName("ID");

        builder.Property(t => t.Created).HasColumnName("CREATED").IsRequired();

        builder.Property(t => t.Updated).HasColumnName("UPDATED");

        builder.Property(t => t.Name).HasColumnName("NAME").IsRequired();

        builder.Property(t => t.Description).HasColumnName("DESCRIPTION");

        builder.Property(t => t.Price).HasColumnName("PRICE").IsRequired();
    }
}