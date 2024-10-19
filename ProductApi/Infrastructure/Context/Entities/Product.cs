using Shared.Dtos.Products;

namespace Infrastructure.Context.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    public Product() { }

    public Product(ProductDto productDto)
    {
        Id = productDto.Id;
        Name = productDto.Name;
        Description = productDto.Description;
        Price = productDto.Price;
        Updated = DateTime.Now;
    }

    public Product(ProductCreateDto productCreateDto)
    {
        Name = productCreateDto.Name;
        Description = productCreateDto.Description;
        Price = productCreateDto.Price;
    }

    public static ProductDto? MapDto(Product? product)
    {
        if (product is null) return null;

        return new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description ?? string.Empty,
            Price = product.Price,
            Created = product.Created,
            Updated = product.Updated
        };
    }

    public static List<ProductDto> MapDtoList(List<Product>? product)
    {
        if (!product?.Any() ?? true)
            return [];

        return product!.Select(MapDto).ToList()!;
    }
}
