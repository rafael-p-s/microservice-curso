namespace Shared.Dtos.Products;

public class ProductDto : BaseDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
