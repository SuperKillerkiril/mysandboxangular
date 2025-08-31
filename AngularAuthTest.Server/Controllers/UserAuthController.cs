using AngularAuthTest.Server.Data;
using AngularAuthTest.Server.Models;
using AngularAuthTest.Server.Models.Dto;
using AngularAuthTest.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthTest.Server.Controllers;

[Route("auth/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]//воход
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var success = await _authService.LoginAsync(model.Email, model.Password);
        if (!success)
            return Unauthorized();

        return Ok();
    }

    [HttpPost("logout")]//выход
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return Ok();
    }

    [HttpPost(Name = "register")]//рега
    public async Task<IActionResult> Registred([FromBody] User user)
    {
        var success = await _authService.RegisterAsync(user);
        if (!success)
            return BadRequest();
        
        return Ok();
    }


    [HttpGet("test")]//я нихуя не понимаю
    public IActionResult Test()
    {
        return Ok("тест");
    }
}