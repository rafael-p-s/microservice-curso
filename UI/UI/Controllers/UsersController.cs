namespace UI.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly INotifierService _notifierService;

    public UsersController(IUserService userService, INotifierService notifierService)
    {
        _userService = userService;
        _notifierService = notifierService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetAll();

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return View("Index", result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _userService.GetUserById(id);

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

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

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _userService.GetUserById(id);

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return View("Edit", result);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserModel user)
    {
        var result = await _userService.UpdateUser(user);

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Delete/{id}/{name}")]
    public IActionResult Delete(int id, string name)
    {
        var result = new UserBaseModel() { Id = id, Name = name };

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return View("Delete", result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);

        if (_notifierService.HasMessages())
        {
            var logs = _notifierService.GetLog();
            ViewData["ErrorLogs"] = logs;
        }

        return RedirectToAction(nameof(Index));
    }
}
