namespace Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ProductApiDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
