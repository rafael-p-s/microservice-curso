namespace Domain.Services;

public class ProductService : IProductService
{
    private readonly IClientService _clientService;
    private readonly INotifierService _notifierService;

    private const string GET_ALL_URL = "https://localhost:7141/Product/GetAll";
    private const string GET_BY_ID_URL = "https://localhost:7141/Product/Details/";
    private const string CREATE_PRODUCT_URL = "https://localhost:7141/Product/Create";
    private const string UPDATE_PRODUCT_URL = "https://localhost:7141/Product/Update";
    private const string DELETE_PRODUCT_BY_ID_URL = "https://localhost:7141/Product/Delete/";

    public ProductService(IClientService clientService, INotifierService notifierService)
    {
        _clientService = clientService;
        _notifierService = notifierService;
    }

    public async Task<List<ProductModel>?> GetAll()
    {
        var result = await _clientService.GetAsync(GET_ALL_URL);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return [];
        }

        return JsonExtensions.DeserializeCustomResponse<List<ProductModel>>(result.Content!);
    }

    public async Task<ProductModel?> GetProductById(int id)
    {
        var url = string.Concat(GET_BY_ID_URL, id);
        var result = await _clientService.GetAsync(url);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<ProductModel>(result.Content!);
    }

    public async Task<ProductModel?> CreateProduct(ProductCreateModel userModel)
    {
        var result = await _clientService.PostAsync(CREATE_PRODUCT_URL, userModel);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<ProductModel>(result.Content!);
    }

    public async Task<ProductModel?> UpdateProduct(ProductModel userModel)
    {
        var result = await _clientService.PutAsync(UPDATE_PRODUCT_URL, userModel);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<ProductModel>(result.Content!);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var url = string.Concat(DELETE_PRODUCT_BY_ID_URL, id);
        var result = await _clientService.DeleteAsync(url);

        if (result is null)
            return new();

        if (result.Status is false)
        {
            _notifierService.AddLog(result.Content?.ToString()!);

            return new();
        }

        return JsonExtensions.DeserializeCustomResponse<bool>(result.Content!);
    }
}
