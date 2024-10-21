namespace UserApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly INotifierService _notifierService;

    public UserController(IUserService userService, INotifierService notifierService)
    {
        _userService = userService;
        _notifierService = notifierService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAll();

        return CustomResponse(result);
    }

    [HttpGet("GetUserRoles/{apiKey}")]
    public async Task<IActionResult> GetUserRolesAsync(string apiKey)
    {
        var result = await _userService.GetUserRolesAsync(apiKey);

        return CustomResponse(result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _userService.GetUserById(id);

        return CustomResponse(result);
    }

    [HttpPost("Create")]
    [Authorize(Roles = $"ADMIN, Create")]
    public async Task<IActionResult> Create(UserBaseDto userDto)
    {
        var result = await _userService.CreateUser(userDto);

        return CustomResponse(result);
    }

    [HttpPut("Update")]
    [Authorize(Roles = $"ADMIN, Update")]
    public async Task<IActionResult> Update(UserDto userDto)
    {
        var result = await _userService.UpdateUser(userDto);

        return CustomResponse(result);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = $"ADMIN, Delete/{{id}}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUser(id);

        return CustomResponse(result);
    }

    private IActionResult CustomResponse(object? content)
    {
        if (_notifierService.HasMessages())
            return BadRequest(new ResponseDto(false, _notifierService.GetLog()));

        return Ok(new ResponseDto(true, content));
    }
}
