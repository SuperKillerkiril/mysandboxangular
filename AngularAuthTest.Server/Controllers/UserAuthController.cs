using AngularAuthTest.Server.Data;
using AngularAuthTest.Server.Models;
using AngularAuthTest.Server.Models.Dto;
using AngularAuthTest.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthTest.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    // POST: /api/auth/login
    [HttpPost(Name = "login")]//воход
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var success = await _authService.LoginAsync(model.Email, model.Password);
        if (!success)
            return Unauthorized();

        return Ok();
    }

    [HttpPost(Name = "logout")]//выход
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

    [HttpGet(Name = "getauthuser")]
    public async Task<User> getByEmail()
    {
        return await _authService.GetUserAsync();
    }


    [HttpGet(Name = "test")]//я нихуя не понимаю
    public IActionResult Test()
    {
        return Ok("тест");
    }
}