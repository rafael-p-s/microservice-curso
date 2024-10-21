namespace Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly INotifierService _notifierService;

    public ProductService(IProductRepository userRepository, INotifierService notifierService)
    {
        _productRepository = userRepository;
        _notifierService = notifierService;
    }

    public async Task<List<ProductDto>> GetAll()
    {
        var result = await _productRepository.GetAll();

        return Product.MapDtoList(result);
    }

    public async Task<ProductDto?> GetProductById(int id)
    {
        if (id < 1)
        {
            _notifierService.AddLog("Invalid Id sent.");
            return null;
        }

        var result = await _productRepository.GetById(id);

        if (result is null)
        {
            _notifierService.AddLog("Product not found in database.");
            return null;
        }

        return Product.MapDto(result);
    }

    public async Task<ProductDto?> CreateProduct(ProductCreateDto productCreateDto)
    {
        var result = await _productRepository.Add(new(productCreateDto));

        if (result is null)
        {
            _notifierService.AddLog("Product cannot be created in database.");
            return null;
        }

        return Product.MapDto(result);
    }

    public async Task<ProductDto?> UpdateProduct(ProductDto userDto)
    {
        var result = await _productRepository.Update(new(userDto));

        if (result is null)
        {
            _notifierService.AddLog("Product cannot be updated in database.");
            return null;
        }

        return Product.MapDto(result);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var getProduct = await _productRepository.GetById(id);

        if (getProduct is null)
        {
            _notifierService.AddLog("Product cannot be found in database.");
            return false;
        }

        var result = await _productRepository.Delete(getProduct);

        if (!result)
        {
            _notifierService.AddLog("Product cannot be deleted from database.");
            return false;
        }

        return true;
    }
}
