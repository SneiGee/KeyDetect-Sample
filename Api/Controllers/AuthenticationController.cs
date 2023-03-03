using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        return null!;
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return null!;
    }
}