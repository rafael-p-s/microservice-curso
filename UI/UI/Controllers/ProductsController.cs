namespace UI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productsService;
    private readonly INotifierService _notifierService;

    public ProductsController(IProductService userService, INotifierService notifierService)
    {
        _productsService = userService;
        _notifierService = notifierService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _productsService.GetAll();
        IncludeMessages();

        return View("Index", result);
    }

    [HttpGet("ProductDetails/{id}")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        var result = await _productsService.GetProductById(id);

        IncludeMessages();

        return View("Details", result);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateModel user)
    {
        if (!VerifyModelState(user))
            return Create();

        var result = await _productsService.CreateProduct(user);

        return await Index();
    }

    [HttpGet("ProductEdit/{id}")]
    public async Task<IActionResult> ProductEdit(int id)
    {
        var result = await _productsService.GetProductById(id);

        IncludeMessages();

        return View("Edit", result);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductModel user)
    {
        if (!VerifyModelState(user))
            return View("Edit", user.Id);

        var result = await _productsService.UpdateProduct(user);

        return await Index();
    }

    [HttpGet("ProductDelete/{id}/{name}")]
    public IActionResult ProductDelete(int id, string name)
    {
        var result = new ProductModel() { Id = id, Name = name };

        IncludeMessages();

        return View("Delete", result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productsService.DeleteProduct(id);

        IncludeMessages();

        return await Index();
    }

    private bool VerifyModelState(ProductModel user)
    {
        if (!ModelState.IsValid)
        {
            LogModelErrors();
            return false;
        }

        return true;
    }

    private bool VerifyModelState(ProductCreateModel user)
    {
        if (!ModelState.IsValid)
        {
            LogModelErrors();
            return false;
        }

        return true;
    }

    private void LogModelErrors()
    {
        foreach (var state in ModelState)
        {
            foreach (var error in state.Value.Errors)
            {
                _notifierService.AddLog(error.ErrorMessage);
            }
        }

        IncludeMessages();
    }

    private void IncludeMessages()
    {
        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }
    }
}
