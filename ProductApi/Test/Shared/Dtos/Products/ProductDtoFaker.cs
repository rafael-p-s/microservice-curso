namespace Test.Shared.Dtos.Products;

public static class ProductDtoFaker
{
    public static ProductDto ProductDtoValid(int id = 1)
    {
        return new()
        {
            Id = id,
            Name = "Unit Testing",
            Description = "Product Description",
            Price = 100
        };
    }

    public static List<ProductDto> ProductDtoList()
    {
        return [ProductDtoValid(), ProductDtoValid(2), ProductDtoValid(88)];
    }
}
