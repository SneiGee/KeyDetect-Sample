using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
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