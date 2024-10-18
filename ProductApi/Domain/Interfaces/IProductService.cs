using Shared.Dtos.Products;

namespace Domain.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAll();
    Task<ProductDto?> GetProductById(int id);
    Task<ProductDto?> CreateProduct(ProductCreateDto productDto);
    Task<ProductDto?> UpdateProduct(ProductDto productDto);
    Task<bool> DeleteProduct(int id);
}
