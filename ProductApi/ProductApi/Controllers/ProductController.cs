namespace ProductApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly INotifierService _notifierService;

    public ProductController(IProductService userService, INotifierService notifierService)
    {
        _productService = userService;
        _notifierService = notifierService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();

        return CustomResponse(result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _productService.GetProductById(id);

        return CustomResponse(result);
    }

    [HttpPost("Create")]
    [Authorize(Roles = $"ADMIN, Create")]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        var result = await _productService.CreateProduct(productCreateDto);

        return CustomResponse(result);
    }

    [HttpPut("Update")]
    [Authorize(Roles = $"ADMIN, Update")]
    public async Task<IActionResult> Update(ProductDto userDto)
    {
        var result = await _productService.UpdateProduct(userDto);

        return CustomResponse(result);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = $"ADMIN, Delete/{{id}}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteProduct(id);

        return CustomResponse(result);
    }

    private IActionResult CustomResponse(object? content)
    {
        if (_notifierService.HasMessages())
            return BadRequest(new ResponseDto(false, _notifierService.GetLog()));

        return Ok(new ResponseDto(true, content));
    }
}
