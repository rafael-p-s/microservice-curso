namespace Test.Infrastructure.Context.Entities;

public class ProductFaker
{
    public static Product ProductValid(int id = 1)
    {
        return new()
        {
            Id = id,
            Name = "Unit Testing",
            Description = "Product Description",
            Price = 100
        };
    }

    public static List<Product> ProductList()
    {
        return [ProductValid(), ProductValid(2), ProductValid(88)];
    }
}
