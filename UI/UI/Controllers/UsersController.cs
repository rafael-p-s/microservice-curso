namespace UI.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetAll();

        return View("Index", result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _userService.GetUserById(id);

        return View("Details", result);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        var result = await _userService.CreateUser(user);

        if (result is null)
            return View("../Home/Error");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _userService.GetUserById(id);

        return View("Edit", result);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserModel user)
    {
        var result = await _userService.UpdateUser(user);

        if (result is null)
            return View("../Home/Error");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Delete/{id}/{name}")]
    public async Task<IActionResult> Delete(int id, string name)
    {
        var result = new UserBaseModel() { Id = id, Name = name };

        return View("Delete", result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);

        if (!result)
            return View("../Home/Error");

        return RedirectToAction(nameof(Index));
    }
}
