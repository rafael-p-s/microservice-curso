namespace UserApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        var result = await _userService.GetAll();

        return Ok(result);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _userService.GetUserById(id);

        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserBaseDto userDto)
    {
        var result = await _userService.CreateUser(userDto);

        return CreatedAtAction(nameof(Details), new { id = result.Id }, result);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UserDto userDto)
    {
        var result = await _userService.UpdateUser(userDto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUser(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
