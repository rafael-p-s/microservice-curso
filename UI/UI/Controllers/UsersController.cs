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
        IncludeMessages();

        return View("Index", result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _userService.GetUserById(id);

        IncludeMessages();

        return View("Details", result);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserBaseModel user)
    {
        if (!VerifyModelState(user))
            return Create();

        var result = await _userService.CreateUser(user);

        return await Index();
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _userService.GetUserById(id);

        IncludeMessages();

        return View("Edit", result);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserModel user)
    {
        if (!VerifyModelState(user))
            return View("Edit", user.Id);

        var result = await _userService.UpdateUser(user);

        return await Index();
    }

    [HttpGet("Delete/{id}/{name}")]
    public IActionResult Delete(int id, string name)
    {
        var result = new UserModel() { Id = id, Name = name };

        IncludeMessages();

        return View("Delete", result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id, string apiKey)
    {
        var result = await _userService.DeleteUser(id, apiKey);

        IncludeMessages();

        return await Index();
    }

    private bool VerifyModelState(UserBaseModel user)
    {
        if (!ModelState.IsValid)
        {
            LogModelErrors();
            return false;
        }

        return true;
    }

    private bool VerifyModelState(UserModel user)
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
