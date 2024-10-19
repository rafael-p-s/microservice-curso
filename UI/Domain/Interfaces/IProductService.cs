namespace Domain.Interfaces;

public interface IProductService
{
    Task<List<ProductModel>?> GetAll();
    Task<ProductModel?> GetProductById(int id);
    Task<ProductModel?> CreateProduct(ProductCreateModel userModel);
    Task<ProductModel?> UpdateProduct(ProductModel userModel);
    Task<bool> DeleteProduct(int id);
}
