using System.Security.Claims;
using AngularAuthTest.Server.Data;
using AngularAuthTest.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthTest.Server.Services;

public class AuthService
{
    private readonly Context _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthService(Context context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user == null)
            return false;
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<bool> RegisterAsync(User newUser)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == newUser.Email);
        if (exists) return false;
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User> GetUserAsync()
    {
        var email = _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Email)?.Value;
        if (email == null)
            return null;
        
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}